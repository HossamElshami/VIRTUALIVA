using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{

    public bool haveQuest = false;
    public int totalGrade = 0;
    public GameObject questPref;
    public GameObject mainObject;
    public static QuestManager instance;
    Color correctColor = Color.green, wrongColor = Color.red;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public int isCorrect(bool _check, int activeStep, int questSteps, int questGrade)
    {
        int step = activeStep;
        if (_check)
        {
            if (step < questSteps)
            {
                UI_Manager.instance.questPanel.transform.GetChild(0).transform.GetChild(activeStep).GetComponent<TMP_Text>().color = correctColor;
                totalGrade += questGrade;
                step++;
            }
        }
        else
        {
            isWrong(activeStep, questSteps, wrongColor);
        }
        return step;
    }

    public bool isFinished(int questSteps ,int activeStep)
    {
        if (activeStep == questSteps)
        {
            return true;
        }
        return false;
    }

    void isWrong(int activeStep, int questSteps, Color wrongColor)
    {
        if (activeStep < questSteps)
            UI_Manager.instance.questPanel.transform.GetChild(0).transform.GetChild(activeStep).GetComponent<TMP_Text>().color = wrongColor;
    }
    public void addQuestStepsToUI(int questSteps, Quest quest)
    {
        for (int i = 0; i < questSteps; i++)
        {
            GameObject q = Instantiate(questPref, UI_Manager.instance.questPanel.transform.GetChild(0).transform);
            //q.GetComponent<TMP_Text>().text = quest._questSteps[i].ID + " - " + quest._questSteps[i].description;
            //q.transform.parent = UI_Manager.instance.questPanel.transform;
        }
    }    
}
