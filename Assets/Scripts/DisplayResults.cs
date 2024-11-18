using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayResults : LoadResults
{
    [SerializeField] private TextMeshProUGUI _gradeText;
    [SerializeField] private TextMeshProUGUI _pointsText;

    private void Start()
    {
        ShowPoints();
        CalculateGrade();
    }

    private void ShowPoints()
    {
        _pointsText.text = "Je score is: " + scoreAndAwnsers.currentPoints.ToString() + " Van de: " + scoreAndAwnsers.maxScore.ToString(); 
    }

    /// <summary>
    /// Calculates the grade by dividing your current points with the max and then multiplying by 10 which will give you a grade out of 10
    /// </summary>
    private void CalculateGrade()
    {
       float grade = scoreAndAwnsers.currentPoints / scoreAndAwnsers.maxScore * 10;
        _gradeText.text = "Je cijfer is: " + grade.ToString() + "/10";
    }
}
