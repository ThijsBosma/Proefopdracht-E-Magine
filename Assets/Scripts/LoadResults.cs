using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResults : MonoBehaviour
{
    protected ScoreAndAwnsers scoreAndAwnsers = new ScoreAndAwnsers();
    
    private void Awake()
    {
        ReadFromJSON();
    }

    private void ReadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/PointsAndAwnsers.json";

        if (System.IO.File.Exists(filePath))
        {
            string recordedData = System.IO.File.ReadAllText(filePath);

            scoreAndAwnsers = JsonUtility.FromJson<ScoreAndAwnsers>(recordedData);
            Debug.Log("Loaded");
        }
        else
        {
            Debug.LogError($"No JSON file found in {filePath}");
        }
    }
}
