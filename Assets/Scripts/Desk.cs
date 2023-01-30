using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    [SerializeField]
    Desk_SO data;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI_Manager.instance.botText.gameObject.SetActive(true);
            //UI_Manager.instance.print_Text(data);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            UI_Manager.instance.botText.gameObject.SetActive(true);
            //UI_Manager.instance.print_Text(data);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        UI_Manager.instance.botText.gameObject.SetActive(false);
    }
}
