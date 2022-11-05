using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{    
    public List<Quest_SO> _questSteps;
    public int stepGrade, totalGrade, questSteps, activeStep;

    public string questName;    

    public bool _isFinished = false, _isActive = false;

    public virtual void check() { }
}
