using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public bool PlayerIsHere;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player") PlayerIsHere = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Player") PlayerIsHere = false;
    }
}
