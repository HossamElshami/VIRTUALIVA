using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestStep))]
public class CustomQuestStepEditor : Editor
{
    SerializedProperty key;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestStep step = (QuestStep)target;
        EditorGUILayout.Space();
        switch (step.stepType)
        {
            case QuestStep.QuestType.ColisionWithOtherObject:
                step.ColisionObject1 = createToolField("First Item", step.ColisionObject1);
                step.ColisionObject2 = createGOField("Second Item", step.ColisionObject2);
                break;
            //case QuestStep.QuestType.MixWithOtherObject:
            //    break;
            case QuestStep.QuestType.OpenPanel:
                step.PanelToOpen = createGOField("Panel", step.PanelToOpen);
                break;
            case QuestStep.QuestType.ClickButton:
                key = serializedObject.FindProperty("ButtonToClick");
                serializedObject.Update();
                EditorGUILayout.PropertyField(key, true);
                serializedObject.ApplyModifiedProperties();
                break;
            case QuestStep.QuestType.GoSomewhere:
                GUILayout.Label("Create triggerbox and put it anywhere want user go to");

                if (GUILayout.Button("Create triggerbox")) step.CreateTriggerEvent();
                break;
            case QuestStep.QuestType.DragObject:
                step.objectToDrag = createToolField("Object", step.objectToDrag);
                break;
            case QuestStep.QuestType.ToolFromInventory:
                step.toolFromInventory = createToolField("Object", step.toolFromInventory);
                break;
        }
    }
    GameObject createGOField(string label, GameObject obj)
    {
        MakeField(label);
        GameObject input = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject), allowSceneObjects: true);
        GUILayout.EndHorizontal();
        return input;
    }
    Tool createToolField(string label, Tool obj)
    {
        MakeField(label);
        Tool input = (Tool)EditorGUILayout.ObjectField(obj, typeof(Tool), allowSceneObjects: true);
        GUILayout.EndHorizontal();
        return input;
    }
    DragableItem createToolField(string label, DragableItem obj)
    {
        MakeField(label);
        DragableItem input = (DragableItem)EditorGUILayout.ObjectField(obj, typeof(DragableItem), allowSceneObjects: true);
        GUILayout.EndHorizontal();
        return input;
    }
    void MakeField(string label)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        GUILayout.FlexibleSpace();
    }

}
