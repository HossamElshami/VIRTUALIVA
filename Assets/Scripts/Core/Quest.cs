using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    [HideInInspector]
    public bool wantToDelete = false;
    public List<QuestStep> Steps;
    public int totalGrade, questSteps, activeStep, questIndex;

    public string questName;
    public string questDescription;
    public List<Tool> questTools;
    public bool _isFinished = false, _isActive = false;
    GameObject newQuest;
    QuestUIManager questUIManager;
    public bool selected = false;
    [SerializeField] Color normalColor, selectedColor;
    Button btn;

    void Start()
    {
        questUIManager = QuestUIManager.instance;
        if (GetComponent<Button>())
        {
            GetComponent<Button>().onClick.AddListener(delegate { showSteps(questIndex); });
        }
    }
    public virtual void check() { }
    public void createNewStep()
    {
        //spawn object
        newQuest = new GameObject("Step " + (Steps.Count + 1));
        //newQuest.name = "Step " + (Steps.Count + 1);
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
        questUIManager.activeQuestBtn.GetComponentInChildren<TMP_Text>().text = QuestManager.instance.ActiveQuest == this ? "Already Selected" : "Select experience";
    }
}