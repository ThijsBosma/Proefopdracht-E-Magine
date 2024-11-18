using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class QuestionInfo : MonoBehaviour
{
    [Header("UI Related")]
    [SerializeField] private GameObject[] _questionGameObjects;
    [SerializeField] private UIDocument[] _questionSheets;
    [SerializeField] private VisualElement visualElement;
    public TextField awnserTextField;
    private Button submitButton;
        
    [Header("Awnsers")]
    [SerializeField, Tooltip("The correct awnser for the question. In order.")] 
    private string[] _awnsers;

    [SerializeField, Tooltip("The amount of points given per question. In order.")]
    private int[] _questionPoints;

    [Header("Questions")]
    [Tooltip("The current question the quiz taker is currently on")]
    private int currentQuestion = 0;

    [SerializeField, Tooltip("The maximum amount of questions for the quiz")]
    private int _maxQuestions;

    private int correctAwnsers;
    private int wrongAwnsers;

    [Header("Classes")]
    private ScoreAndAwnsers scoreAndAwnsers = new ScoreAndAwnsers();

    private void OnEnable()
    {
        SetQuestionActive();
        SubmitAwnser();
    }

    /// <summary>
    /// Gets the required data for the textfield
    /// </summary>
    private void GetTextFieldData()
    {
        awnserTextField = visualElement.Q<TextField>("Awnser");
        scoreAndAwnsers.awnsers.Add(awnserTextField.text);
    }

    /// <summary>
    /// The function checks the textfields and compares it to the given awnser
    /// </summary>
    private void CheckForCorrectAwnser()
    {
        if (awnserTextField.value.ToLower() == _awnsers[currentQuestion].ToLower())
        {
            correctAwnsers += 1;
            scoreAndAwnsers.currentPoints += _questionPoints[currentQuestion];
            currentQuestion += 1;
            Debug.Log("Correct awnser");
            SetQuestionActive();
            SubmitAwnser();
        }
        else
        {
            wrongAwnsers += 1;
            currentQuestion += 1;
            Debug.Log("False Awnser");
            SetQuestionActive();
            SubmitAwnser();
        }
    }

    /// <summary>
    /// When pressing the button this code activates
    /// </summary>
    private void SubmitAwnser()
    {
        submitButton = visualElement.Q<Button>("Submit");
        submitButton.RegisterCallback<ClickEvent>(evt =>
        {
            GetTextFieldData();
            CheckForCorrectAwnser();
        });
    }

    /// <summary>
    /// Loads the scene that shows your points and grade
    /// </summary>
    private void FinishedQuiz()
    {
        if(currentQuestion >= _maxQuestions)
        {
            WriteToJSON();
            SceneManager.LoadScene("EvaluationScreen");
        }
    }

    /// <summary>
    /// Sets the current question active and checks for out of bounds
    /// </summary>
    private void SetQuestionActive()
    {
        if (currentQuestion == 0)
        {
            _questionGameObjects[currentQuestion].SetActive(true);
        }
        else if(!IsIndexOutOfBounds())
        {
            _questionGameObjects[currentQuestion - 1].SetActive(false);
            _questionGameObjects[currentQuestion].SetActive(true);
        }
        else if(IsIndexOutOfBounds())
        {
            FinishedQuiz();
        }

        visualElement = _questionSheets[currentQuestion].rootVisualElement;
        Debug.Log(visualElement);
    }

    /// <summary>
    /// Checks to see if the index is out of bounds.
    /// </summary>
    /// <returns></returns>
    private bool IsIndexOutOfBounds()
    {
        if (currentQuestion >= _maxQuestions)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Saves the score you get and the awnsers you typed to JSON locally
    /// </summary>
    private void WriteToJSON()
    {
        string recordedPlayerMovements = JsonUtility.ToJson(scoreAndAwnsers, true);
        string filePath = Application.persistentDataPath + "/PointsAndAwnsers.json";

        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, recordedPlayerMovements);
        Debug.Log("Files Saved");
    }
}
