using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayResults : LoadResults
{
    [SerializeField] private TextMeshProUGUI _gradeText;

    private void Start()
    {
        CalculateGrade();
    }

    private void CalculateGrade()
    {
       float grade = scoreAndAwnsers.currentPoints / scoreAndAwnsers.maxScore * 10;
        _gradeText.text = "Je cijfer is: " + grade.ToString();
    }
}
