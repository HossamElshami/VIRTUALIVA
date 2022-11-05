using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rayPoint;
    [SerializeField]
    private GameObject selectedObject;

    bool isDragging = false;
    [SerializeField]
    LayerMask draggingItemLayer;

    private void Update()
    {
        if (selectedObject)
        {
            if (Vector3.Distance(transform.position, selectedObject.transform.position) < 5 || isDragging)
            {
                if (Input.GetMouseButtonDown(0))
                    isDragging = true;
                else if (Input.GetMouseButtonUp(0))
                    isDragging = false;
                DragObject();
                UI_Manager.instance.pcName.gameObject.SetActive(false);
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    UI_Manager.instance.print_Text("Object too far to drag it");
                    UI_Manager.instance.pcName.gameObject.SetActive(Input.GetMouseButton(0));
                }
            }            
        }else
            UI_Manager.instance.pcName.gameObject.SetActive(false);
        if (!isDragging)
        {
            MakeRayCast();
        }
    }
    void MakeRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, draggingItemLayer))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Dragable")
                {
                    selectedObject = hit.collider.gameObject;
                        if (!selectedObject.GetComponent<Outline>().enabled)
                        {
                            selectedObject.GetComponent<Outline>().enabled = true;
                        }
                        selectedObject.GetComponent<Outline>().enabled = true;
                        if (hit.collider.gameObject != selectedObject)
                            selectedObject.GetComponent<Outline>().enabled = false;                                                                   
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
        selectedObject.transform.parent = isDragging ? Camera.main.transform : selectedObject.GetComponent<DragableItem>().parent;
    }
}
