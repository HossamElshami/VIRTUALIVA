using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    [HideInInspector]
    public bool wantToDelete = false;
    public List<QuestStep> Steps = new List<QuestStep>();
    public int totalGrade, questSteps, activeStep, questIndex, questGrade;

    public string questName;
    [TextArea]
    public string questDescription;
    public List<Tool> questTools;
    public bool _isFinished = false, _isActive = false;
    QuestUIManager questUIManager;
    public QuestManager QM;
    public bool selected = false;
    [SerializeField] Color normalColor, selectedColor;
    Button btn;

    void Start()
    {
        questUIManager = QuestUIManager.instance;
        if (GetComponent<Button>())
            GetComponent<Button>().onClick.AddListener(delegate { showSteps(questIndex); });

        for (int i = 0; i < Steps.Count; i++)
            questGrade += Steps[i].stepGrade;
    }
    public void ActiveNextStep(int StepID, int grade)
    {
        totalGrade += grade;
        if (Steps.Count > StepID + 1)
            Steps[StepID + 1]._isActive = true;
        else
        {
            UI_Manager.instance.botPrint("The experiment \"" + questName + "\" has finished! Your grade: " + questGrade + " / " + totalGrade, 5f);
            QuestManager.instance.endQuest();
        }
    }
    public void createNewStep()
    {
        GameObject newQuest;
        newQuest = new GameObject("Step " + (Steps.Count + 1));
        newQuest.transform.parent = this.transform;
        newQuest.AddComponent<QuestStep>();
        if (Steps.Count < 1)
            newQuest.GetComponent<QuestStep>()._isActive = true;
        QM = transform.parent.transform.parent.GetComponent<QuestManager>();
        Steps.Add(newQuest.GetComponent<QuestStep>());
        newQuest.GetComponent<QuestStep>().StepID = Steps.Count - 1;
    }
    public void clearSteps()
    {
        for (int i = 0; i < Steps.Count; i++)
        {
            DestroyImmediate(Steps[i].gameObject);
        }
        Steps.Clear();
    }

    public void showSteps(int questIndex)
    {
        if (questUIManager.QuestStepsContainer.childCount > 0)
        {
            foreach (Transform child in questUIManager.QuestStepsContainer)
                GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < Steps.Count; i++)
        {
            GameObject s = Instantiate(questUIManager.QuestStepCellPrefab, questUIManager.QuestStepsContainer.transform);
            s.GetComponent<TMP_Text>().text = (i + 1) + "-" + Steps[i].StepDescription;
        }
        selectedQuest();
    }
    void selectedQuest()
    {
        ColorBlock cb;
        for (int i = 0; i < questUIManager.questsButtons.Count; i++)
        {
            questUIManager.questsButtons[i].selected = false;
            Button b = questUIManager.questsButtons[i].GetComponent<Button>();
            cb = b.colors;
            cb.normalColor = normalColor;
            b.colors = cb;
        }

        selected = true;
        GetComponent<Image>().color = selectedColor;
        btn = GetComponent<Button>();
        cb = btn.colors;
        cb.normalColor = selectedColor;
        btn.colors = cb;
        questUIManager.selectedQuest = this;
        questUIManager.activeQuestBtn.interactable = QuestManager.instance.ActiveQuest == this ? false : true;
        questUIManager.activeQuestBtn.GetComponentInChildren<TMP_Text>().text = QuestManager.instance.ActiveQuest == this ? "Selected" : "Select experience";
    }
}