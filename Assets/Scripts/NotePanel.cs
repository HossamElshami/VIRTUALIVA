using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePanel : MonoBehaviour
{
    public string message;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI_Manager.instance.pcName.gameObject.SetActive(true);
            UI_Manager.instance.print_Text(message);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            UI_Manager.instance.pcName.gameObject.SetActive(true);
            UI_Manager.instance.print_Text(message);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        UI_Manager.instance.pcName.gameObject.SetActive(false);
    }
}
