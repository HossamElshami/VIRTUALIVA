using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    [HideInInspector]
    public bool wantToDelete = false;
    public List<QuestStep> Steps;
    public int totalGrade, questSteps, activeStep;

    public string questName;    
    public string questDescription;
    public List<Tool> questTools;

    public bool _isFinished = false, _isActive = false;

    public virtual void check() { }
    public void createNewStep()
    {
        GameObject newQuest;
        //spawn object
        newQuest = new GameObject("Step " + (Steps.Count + 1));
        //Add Components
        newQuest.transform.parent = this.transform;
        newQuest.AddComponent<QuestStep>();        
        Steps.Add(newQuest.GetComponent<QuestStep>());
    }
    public void clearSteps()
    {
        for (int i = 0; i < Steps.Count; i++)
        {
            DestroyImmediate(Steps[i].gameObject);
        }
        Steps.Clear();
    }


}