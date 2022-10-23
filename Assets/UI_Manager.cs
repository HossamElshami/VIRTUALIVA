using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text pcName;

    public static UI_Manager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);        
    }
    public void print_Text(Desk_SO data)
    {
          pcName.text = data.Name + "-" + data.PCNumber + " (" + data.LabName + "-" + data.LabNumber + ")";
    }
    public void print_Text(string text)
    {
        pcName.text = text;
    }

}
