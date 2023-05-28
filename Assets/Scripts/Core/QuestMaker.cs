using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMaker : MonoBehaviour
{
    public List<Quest> quests;

    public static QuestMaker instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void createNewQuest(string questName)
    {
        GameObject newQuest;
        //spawn object
        newQuest = new GameObject(questName);
        //Add Components
        newQuest.transform.parent = this.transform.GetChild(0).transform;
        newQuest.AddComponent<Quest>().questName = questName;
        quests.Add(newQuest.GetComponent<Quest>());
    }
}
