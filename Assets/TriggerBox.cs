using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public bool PlayerIsHere;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player") PlayerIsHere = true;
    }
}
