using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestable
{
    public string questName { get; set; }
    public int questSteps { get; set; }
    public int stepGrade { get; set; }
    public int totalGrade { get; set; }

    public void check();
    public void isCorrect(bool check);
    public void isWrong();
    public void isFinished();
}
