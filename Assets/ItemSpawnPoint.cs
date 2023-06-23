using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    public bool isFree = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null) isFree = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == null) isFree = true;
    }
}
