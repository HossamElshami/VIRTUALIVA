using UnityEngine;

public class Tool : MonoBehaviour
{
    public string toolName;
    public string description;
    public bool IsCollisionObject = false;

    public GameObject collisionObject;
    public Tool_SO toolData;
    private void Start()
    {
        if (!toolData) return;
        toolName = toolData.toolName;
        description = toolData.toolDescription;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collisionObject) return;
        if (!collision.gameObject.GetComponent<Tool>()) return;
        if (collision.gameObject.GetComponent<Tool>().toolData.toolName == collisionObject.GetComponent<Tool>().toolData.toolName)
        {
            IsCollisionObject = true;
        }
    }
}
