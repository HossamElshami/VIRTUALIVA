using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestMaker))]
public class CustomQuestMakerEditor : Editor
{
    string questName;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestMaker quest = (QuestMaker)target;

        EditorGUILayout.Space(10);
        GUILayout.Label("Create new quest");
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Quest Name");
        GUILayout.FlexibleSpace();
        questName = EditorGUILayout.TextField("", questName);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create quest"))
        {
            bool isExist = false;
            if (string.IsNullOrEmpty(questName)) Debug.Log("Please enter the quest name first and try again");
            else
            {
                foreach (var _quest in quest.quests)
                {
                    isExist = _quest.name == questName ? true : false;
                }

                if (isExist)
                {
                    Debug.LogWarning("This quest is already exist");
                }
                else if (questName != string.Empty && !isExist)
                {
                    quest.createNewQuest(questName);
                    Selection.activeGameObject = quest.quests[quest.quests.Count - 1].gameObject;
                    questName = "";
                }
                else Debug.LogWarning("You should input quest name first");
            }
        }
    }
}
