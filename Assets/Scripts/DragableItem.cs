using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class DragableItem : MonoBehaviour, Item
{
    Outline outline;
    public bool isDragging = false;

    public bool onTable = false;
    Rigidbody rb;
    new Collider collider;
    public Transform parent;
    Quaternion rot;
    float rotY;
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
    }
    public void use()
    {
        rb.useGravity = !isDragging;
        collider.isTrigger = isDragging;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Table")
        {
            onTable = true;
        }
        else
        {
            onTable = false;
        }
    }
}
