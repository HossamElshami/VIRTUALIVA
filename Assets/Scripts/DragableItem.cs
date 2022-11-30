using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class DragableItem : MonoBehaviour, Item
{
    public bool isDragging = false, onTable = false;

    float rotY;
    Rigidbody rb;
    Quaternion rot;
    Outline outline;
    new Collider collider;
    public Transform parent;    

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 20;
        rb = GetComponent<Rigidbody>();        
        collider = GetComponent<Collider>();
        rot = transform.rotation;
        if (transform.parent) parent = transform.parent;
    }
    private void Update()
    {        
        use();
        if (!isDragging) return;
        itemOptions();
    }
    public void use()
    {
        rb.useGravity = !isDragging;
        rb.isKinematic = isDragging;
        collider.isTrigger = isDragging;       
        outline.enabled = this.gameObject == LabManager.instance.character.GetComponent<DragingSystem>().selectedObject ? true : false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        onTable = collision.transform.tag == "Table" ? true : false;
    }
    void itemOptions()
    {
        if (InputManager.instance.inputDown(KeyCode.E))
        {
            transform.rotation = LabManager.instance.character.GetComponent<DragingSystem>().itemTransform.rotation;
            transform.position = LabManager.instance.character.GetComponent<DragingSystem>().itemTransform.position;
            transform.localScale = Vector3.one;
        }
        if (InputManager.instance.inputDown(KeyCode.R))
        {
            transform.Rotate(Vector3.up * 45);
        }
        if (InputManager.instance.inputDown(KeyCode.T))
        {
            transform.localScale *= 1.25f;
        }
    }
}
