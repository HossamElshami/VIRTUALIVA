using UnityEngine;

public class DragingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rayPoint;
    public GameObject selectedObject { get; private set; }
    [field:SerializeField] public Transform itemTransform { get; private set; }

    public bool isDragging = false;
    Vector3 hitPoint;
    [SerializeField]
    LayerMask draggingItemLayer;
    public Transform shadow;
    UI_Manager UI;

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
            UI.ItemNameContainer.SetActive(true);
            if(!EditItemPage.instance.isEditting){
                UI.ItemNameText.text = selectedObject.GetComponent<Tool>() != null ?
                selectedObject.GetComponent<Tool>().toolData.toolName + (isDragging ? (!UI.optionPanel.activeInHierarchy ? " (Click [E] to edit)" : " (Click [E] to out)") : "") :
                selectedObject.name + (isDragging ? (!UI.optionPanel.activeInHierarchy ? " (Click [E] to edit)" : " (Click [E] to out)") : "");
            }
            if (Vector3.Distance(transform.position, hitPoint) < 5 || isDragging)
            {
                if (InputManager.instance.inputDown(KeyCode.Mouse0) && !EditItemPage.instance.isEditting)
                    isDragging = !isDragging;
                DragObject();
                UI.botText.gameObject.SetActive(false);
            }
            else
                if (InputManager.instance.LeftMouseDown) UI.botPrint("Object too far to drag it");
        }
        else
        {
            UI.botPanel.SetActive(false);
            UI.ItemNameText.text = string.Empty;
            UI.ItemNameContainer.SetActive(false);
        }
        if (!isDragging) MakeRayCast();
    }
    void MakeRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.instance.MousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, draggingItemLayer))
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
