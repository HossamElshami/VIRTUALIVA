using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMaker : MonoBehaviour
{
    public List<GameObject> quests;
    public void createNewQuest(string questName)
    {
        GameObject newQuest;
        //spawn object
        newQuest = new GameObject(questName);
        //Add Components
        newQuest.transform.parent = this.transform.GetChild(0).transform;
        newQuest.AddComponent<Quest>().questName = questName;
        quests.Add(newQuest);
    }
}
