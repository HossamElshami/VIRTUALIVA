using System;
using UnityEditor;
using UnityEngine;
//[Serializable]
public class QuestStep : MonoBehaviour
{
    [HideInInspector]
    public int StepID;
    public int stepGrade;
    public string StepName;
    public enum QuestType { ColisionWithOtherObject, /*MixWithOtherObject,*/ OpenPanel, ClickButton, GoSomewhere, DragObject }
    [TextArea]
    public string StepDescription;
    public bool _isFinished = false, _isActive = false;
    [Header("Type of step")]
    [Space(20)]
    public QuestType stepType;
    [HideInInspector]
    public Tool ColisionObject1;
    [HideInInspector]
    public GameObject ColisionObject2;
    [HideInInspector]
    public GameObject PanelToOpen;
    [HideInInspector]
    public KeyCode ButtonToClick;
    [HideInInspector]
    public GameObject triggerBox;
    [HideInInspector]
    public DragableItem objectToDrag;

    void Update()
    {
        if (!_isActive) return;

        switch (stepType)
        {
            case QuestType.ColisionWithOtherObject:
                ColisionWithObject();
                break;
            case QuestType.OpenPanel:
                OpenPanel();
                break;
            case QuestType.ClickButton:
                ClickButton();
                break;
            case QuestType.GoSomewhere:
                GoSomewhere();
                break;
            case QuestType.DragObject:
                DraggingObject();
                break;
        }
    }
    public void CreateTriggerEvent()
    {
        GameObject box;
        //spawn object
        box = new GameObject("BoxTrigger");
        //Add Components
        box.transform.parent = transform;
        box.AddComponent<BoxCollider>().isTrigger = true;
        box.AddComponent<Rigidbody>().useGravity = false;
        box.AddComponent<TriggerBox>();
        triggerBox = box;
    }
    void ColisionWithObject()
    {
        //check if object is the second item
        if (!ColisionObject1 | !ColisionObject2) return;

        ColisionObject1.collisionObject = ColisionObject2;
        //check if first item colision with second item
        //make the step is finished
        _isFinished = ColisionObject1.IsCollisionObject;
    }
    void OpenPanel()
    {
        if (!PanelToOpen) return;
        _isFinished = PanelToOpen.activeInHierarchy;
    }
    void ClickButton()
    {
        if (ButtonToClick == KeyCode.None) return;
        if (Input.GetKeyDown(ButtonToClick)) _isFinished = true;
    }
    void GoSomewhere()
    {
        if (!triggerBox) return;
        _isFinished = triggerBox.GetComponent<TriggerBox>().PlayerIsHere ? true : false;
    }
    void DraggingObject()
    {
        if (!objectToDrag) return;
        _isFinished = objectToDrag.isDragging;
    }
}
