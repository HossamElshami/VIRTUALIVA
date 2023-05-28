using UnityEngine;

[RequireComponent(typeof(Tool))]
[RequireComponent(typeof(Outline))]
public class DragableItem : MonoBehaviour, Item
{
    public bool isDragging = false;

    float rotY;
    Rigidbody rb;
    Quaternion rot;
    Outline outline;
    new Collider collider;
    public Transform parent;
    DragingSystem dragingSystem;

    private void Start()
    {
        if (transform.parent) parent = transform.parent;
        outline = GetComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 20;
        rb = GetComponent<Rigidbody>();        
        collider = GetComponent<Collider>();
        rot = transform.rotation;
        dragingSystem = LabManager.instance.character.GetComponent<DragingSystem>();        
    }
    private void Update()
    {        
        use();
    }
    public void use()
    {
        rb.useGravity = !isDragging;
        rb.isKinematic = isDragging;
        collider.isTrigger = isDragging;       
        outline.enabled = this.gameObject == dragingSystem.selectedObject ? true : false;
    }   
}
