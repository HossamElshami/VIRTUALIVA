using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Quest))]
public class CustomQuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Quest quest = (Quest)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create step"))
        {
            quest.createNewStep();
        }
        if (GUILayout.Button("Delete steps"))
        {
            if (quest.Steps.Count == 0) return;
            quest.wantToDelete = !quest.wantToDelete;
        }
        EditorGUILayout.EndHorizontal();

        if (quest.wantToDelete)
        {
            EditorGUILayout.LabelField("Do you sure to delete quest steps?");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Yes"))
            {
                quest.clearSteps();
                quest.wantToDelete = false;
            }
            if (GUILayout.Button("No"))
            {
                quest.wantToDelete = false;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
