using UnityEngine;
public class DragingSystem : MonoBehaviour
{
    UI_Manager UI;
    [SerializeField]
    private GameObject rayPoint;
    public GameObject selectedObject { get; private set; }
    [field: SerializeField] public Transform itemTransform { get; private set; }
    Vector3 hitPoint;
    [SerializeField]
    LayerMask draggingItemLayer;
    [SerializeField]
    LayerMask ignoreLayer;
    public Transform shadow;
    public bool isDragging = false;
    public static DragingSystem instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        UI = UI_Manager.instance;
    }
    private void Update()
    {
        if (UI.panelOpen) return;
        shadow.gameObject.SetActive(isDragging);
        if (selectedObject)
        {
            UI.showMessageHint = true;
            UI.ItemNameContainer.SetActive(true);
            if (!EditItemPage.instance.isEditting)
            {
                UI.ItemNameText.text = selectedObject.GetComponent<Tool>() != null ?
                    selectedObject.GetComponent<Tool>().toolData.toolName + (isDragging ?
                    (!UI.optionPanel.activeInHierarchy ? " (Click [E] to edit)" : " (Click [E] to stop editing)") : "") :
                    selectedObject.name + (isDragging ? (!UI.optionPanel.activeInHierarchy ? " (Click [E] to edit)" : " (Click [E] to stop editing)") : "");
            }
            if (Vector3.Distance(transform.position, hitPoint) < 5 || isDragging)
            {
                if (InputManager.instance.inputDown(KeyCode.Mouse0) && !EditItemPage.instance.isEditting)
                {
                    isDragging = !isDragging;
                    AudioManager.instance.Play("PickUp");
                    UI.botText.gameObject.SetActive(false);
                }
                DragObject();
            }
            else
                if (InputManager.instance.LeftMouseDown) UI.botPrint("Object too far to drag it, please come closer");
        }
        else if (!Inventory.instance.inRange && !selectedObject)
        {
            //UI.botPanel.SetActive(false);
            UI.showMessageHint = false;
            if (!UI.showMessageHint)
                UI.HideHint();
        }
        if (!isDragging) MakeRayCast();
    }
    void MakeRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.instance.MousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, draggingItemLayer, ~ignoreLayer))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Dragable")
                {
                    selectedObject = hit.collider.gameObject;
                    hitPoint = hit.point;
                }
                else if (selectedObject)
                {
                    selectedObject.GetComponent<Outline>().enabled = false;
                    selectedObject = null;
                }
            }
        }
    }
    void DragObject()
    {
        selectedObject.GetComponent<DragableItem>().isDragging = isDragging;
        selectedObject.transform.parent = isDragging ? itemTransform : selectedObject.GetComponent<DragableItem>().parent;
        if (isDragging) EditItemPage.instance.mainObject = selectedObject;
        RaycastHit hit;
        if (Physics.Raycast(selectedObject.transform.position, Vector3.down, out hit))
            shadow.position = new Vector3(selectedObject.transform.position.x, hit.point.y + 0.1f, selectedObject.transform.position.z);
    }
}
