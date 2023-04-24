using System.Collections;
using System.Collections.Generic;
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

    public static DragingSystem instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (UI_Manager.instance.panelOpen) return;
        shadow.gameObject.SetActive(isDragging);
        if (selectedObject)
        {
            UI_Manager.instance.hoverItemNameText.text = selectedObject.name;
            if (Vector3.Distance(transform.position, hitPoint) < 5 || isDragging)
            {
                if (InputManager.instance.inputDown(KeyCode.Mouse0))
                    isDragging = !isDragging;
                DragObject();
                UI_Manager.instance.botText.gameObject.SetActive(false);
            }
            else
            {
                if (InputManager.instance.LeftMouseDown)
                {
                    UI_Manager.instance.botPrint("Object too far to drag it");
                    //UI_Manager.instance.botPanel.gameObject.SetActive(InputManager.instance.LeftMouseDown);
                }
            }
        }
        else
        {
            UI_Manager.instance.botPanel.SetActive(false);
            UI_Manager.instance.hoverItemNameText.text = string.Empty;
        }
        if (!isDragging)
        {
            MakeRayCast();
        }

        UI_Manager.instance.dragingPanel.SetActive(isDragging && !UI_Manager.instance.optionPanel.activeInHierarchy);
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
        if (!UI_Manager.instance.itemPanel.activeInHierarchy)
        {
            UI_Manager.instance.closePanels();
            //UI_Manager.instance.itemPanel.SetActive(true);
        }
        selectedObject.GetComponent<DragableItem>().isDragging = isDragging;
        selectedObject.transform.parent = isDragging ? itemTransform : selectedObject.GetComponent<DragableItem>().parent;
        if (isDragging) EditItemPage.instance.mainObject = selectedObject;
        RaycastHit hit;
        if (Physics.Raycast(selectedObject.transform.position, Vector3.down, out hit))
            shadow.position = new Vector3(selectedObject.transform.position.x, hit.point.y + 0.1f, selectedObject.transform.position.z);            
    }
}
