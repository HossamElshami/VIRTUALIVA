using TMPro;
using UnityEngine;
public class QuestStep : MonoBehaviour
{
    public int StepID, stepGrade;
    public string StepName;
    public enum QuestType { ColisionWithOtherObject, MixLiquid, OpenPanel, ClickButton, GoSomewhere, DragObject, ToolFromInventory, ChemicalFromInventory }
    [TextArea]
    public string StepDescription;
    public bool _isFinished = false, _isActive = false;
    [Header("Type of step")]
    [Space(20)]
    public QuestType stepType;
    [HideInInspector] public Tool ColisionObject1;
    [HideInInspector] public GameObject ColisionObject2;
    [HideInInspector] public GameObject PanelToOpen;
    [HideInInspector] public Tool toolFromInventory;
    [HideInInspector] public Chemical chemicalFromInventory;
    [HideInInspector] public KeyCode ButtonToClick;
    [HideInInspector] public GameObject triggerBox;
    [HideInInspector] public GameObject WantedLiquid;
    [HideInInspector] public DragableItem objectToDrag;
    GameObject effect;
    Quest quest;

    void Start()
    {
        quest = transform.parent.GetComponent<Quest>();
    }
    void Update()
    {
        if (!quest._isActive || quest._isFinished) return;
        if (!_isActive) return;
        if (QuestManager.instance.questSteps.Count > 0)
            QuestManager.instance.questSteps[StepID].GetComponent<TMP_Text>().color = Color.green;
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
                if (effect == null)
                    effect = Instantiate(LabManager.instance.locationEffect, triggerBox.transform);
                if (_isFinished)
                    Destroy(effect);
                break;
            case QuestType.DragObject:
                DraggingObject();
                break;
            case QuestType.MixLiquid:
                MixLiquids();
                break;
            case QuestType.ToolFromInventory:
                ToolFromInventory();
                break;
            case QuestType.ChemicalFromInventory:
                ChemicalFromInventory();
                break;
        }
        if (_isFinished)
        {
            if (transform.parent.GetComponent<Quest>().Steps.Count > StepID)
            {
                transform.parent.GetComponent<Quest>().ActiveNextStep(StepID, stepGrade);
                QuestManager.instance.questSteps[StepID].GetComponent<TMP_Text>().color = Color.red;
                _isActive = false;
            }
        }
    }
    public void CreateTriggerEvent()
    {
        GameObject box;
        box = new GameObject("BoxTrigger");
        box.transform.parent = transform;
        box.AddComponent<BoxCollider>().isTrigger = true;
        box.AddComponent<Rigidbody>().useGravity = false;
        box.AddComponent<TriggerBox>();
        box.GetComponent<BoxCollider>().enabled = false;
        triggerBox = box;
    }
    void ColisionWithObject()
    {
        if (!ColisionObject1 | !ColisionObject2) return;

        ColisionObject1.collisionObject = ColisionObject2;
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
        triggerBox.GetComponent<BoxCollider>().enabled = _isActive;
        _isFinished = triggerBox.GetComponent<TriggerBox>().PlayerIsHere ? true : false;
    }
    void DraggingObject()
    {
        if (!objectToDrag) return;
        _isFinished = objectToDrag.isDragging;
    }
    void ToolFromInventory()
    {
        if (!InventorySystem.instance.LastInstantiatedTool) return;
        if (toolFromInventory.toolData.toolName == InventorySystem.instance.LastInstantiatedTool.GetComponent<Tool>().toolData.toolName)
            _isFinished = true;
    }
    void ChemicalFromInventory()
    {
        if (!InventorySystem.instance.LastInstantiatedTool) return;
        if (chemicalFromInventory.data._name == InventorySystem.instance.LastInstantiatedTool.GetComponent<Chemical>().data._name)
            _isFinished = true;
    }
    void MixLiquids()
    {
        if (!LiquidMixer.instance.outputLiquid) return;
        if (LiquidMixer.instance.outputLiquid.GetComponent<Chemical>().data._name == WantedLiquid.GetComponent<Chemical>().data._name)
            _isFinished = true;
    }
}
