using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public bool haveQuest = false;
    public int totalGrade = 0;
    public GameObject questPref, stepPref;
    public GameObject mainObject;
    public Quest ActiveQuest;
    Color correctColor = Color.green, wrongColor = Color.red;
    public List<Quest> quests;
    public List<GameObject> questSteps;
    public Color NormalStepColor, ActiveStepColor, DoneStepColor;
    public static QuestManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        quests = QuestMaker.instance.quests;
    }
    void Update()
    {
        UI_Manager.instance.questPanel.transform.GetChild(0).gameObject.SetActive(ActiveQuest);
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

    public bool isFinished(int questSteps, int activeStep)
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
    public void addQuestStepsToUI(Quest quest)
    {
        questSteps.Clear();
        foreach (Transform child in UI_Manager.instance.questPanel.transform.GetChild(0).transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < quest.Steps.Count; i++)
        {
            quest.Steps[i]._isFinished = quest.Steps[i]._isActive = false;
            GameObject q = Instantiate(stepPref, UI_Manager.instance.questPanel.transform.GetChild(0).transform);
            q.GetComponent<TMP_Text>().text = (quest.Steps[i].StepID + 1) + " - " + quest.Steps[i].StepDescription;
            questSteps.Add(q);
        }
        quest.Steps[0]._isActive = true;
    }
    public void endQuest()
    {
        foreach (Transform child in UI_Manager.instance.questPanel.transform.GetChild(0))
            GameObject.Destroy(child.gameObject);
        ActiveQuest._isActive = false;
        ActiveQuest = null;
    }
}
