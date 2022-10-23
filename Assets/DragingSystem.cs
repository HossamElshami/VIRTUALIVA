using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rayPoint;
    [SerializeField]
    private GameObject selectedObject;
    public Material selectedMat;
    Material defaultMat;

    bool isDragging = false;

    private void Update()
    {
        if (selectedObject)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isDragging = true;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isDragging = false;
            }
            DragObject();
        }

        if (!isDragging)
        {
            MakeRayCast();
        }
    }
    void MakeRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Renderer>() != null && hit.collider.tag == "Dragable")
                {
                    selectedObject = hit.collider.gameObject;
                    if (defaultMat == null)
                    {
                        defaultMat = selectedObject.gameObject.GetComponent<Renderer>().material;
                    }
                    selectedObject.GetComponent<Renderer>().material = selectedMat;
                    if (hit.collider.gameObject != selectedObject)
                        selectedObject.GetComponent<Renderer>().material = defaultMat;                    
                }
                else if(selectedObject)
                {
                    selectedObject.GetComponent<Renderer>().material = defaultMat;
                    selectedObject = null;
                    defaultMat = null;
                }
            }
        }
    }
    void DragObject()
    {
        selectedObject.transform.parent = isDragging ? Camera.main.transform : null;
        selectedObject.GetComponent<Rigidbody>().isKinematic = isDragging;
        selectedObject.GetComponent<BoxCollider>().isTrigger = isDragging;
    }
}
