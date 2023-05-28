using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestUIManager : MonoBehaviour
{
    public Button activeQuestBtn;
    public Transform QuestsContainer, QuestStepsContainer;
    public GameObject QuestCellPrefab, QuestStepCellPrefab;
    [SerializeField] QuestManager QM;
    public List<Quest> questsButtons;
    public Quest selectedQuest;
    public static QuestUIManager instance;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        QM = QuestManager.instance;

        foreach (Transform child in QuestsContainer)
            GameObject.Destroy(child.gameObject);

        foreach (Transform child in QuestStepsContainer)
            GameObject.Destroy(child.gameObject);

        LoadQuests();
    }
    void LoadQuests()
    {
        for (int i = 0; i < QuestMaker.instance.quests.Count; i++)
        {
            GameObject q = Instantiate(QuestCellPrefab, QuestsContainer.transform);
            q.GetComponent<Quest>().Steps = QuestMaker.instance.quests[i].Steps;
            q.GetComponent<Quest>().questIndex = i;
            q.transform.GetComponentInChildren<TMP_Text>().text = QuestMaker.instance.quests[i].questName;
            questsButtons.Add(q.GetComponent<Quest>());
        }
    }
    public void ActiveQuest()
    {
        QuestManager.instance.ActiveQuest = selectedQuest;
        activeQuestBtn.interactable = QuestManager.instance.ActiveQuest == selectedQuest ? false : true;
        activeQuestBtn.GetComponentInChildren<TMP_Text>().text = QuestManager.instance.ActiveQuest == selectedQuest ? "Already Selected" : "Select experience";
    }
}
