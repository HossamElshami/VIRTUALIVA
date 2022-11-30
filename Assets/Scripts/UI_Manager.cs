using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public GameObject menu;
    public GameObject questPanel;
    public GameObject inventoryPanel;
    public GameObject itemPanel;
    public List<GameObject> panels;
    public GameObject _quest;
    public TMP_Text pcName;
    public bool panelOpen { get; private set; }

    public static UI_Manager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);        
    }
    private void Update()
    {
        checkPanelsOpen();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeInHierarchy);
            Cursor.lockState = menu.activeInHierarchy ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = menu.activeInHierarchy;
        }
        questPanel.SetActive(!menu.activeInHierarchy);
        if (Input.GetKeyDown(KeyCode.Q)) inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
    public void print_Text(Desk_SO data)
    {
          pcName.text = data.Name + "-" + data.PCNumber + " (" + data.LabName + "-" + data.LabNumber + ")";
    }
    public void print_Text(string text)
    {
        pcName.text = text;
    }
    public void AddQuestToPanel()
    {
        questPanel.SetActive(true);
    }
    void checkPanelsOpen()
    {
        for(int i = 0; i < panels.Count; i++)
        {
            if (panels[i].activeInHierarchy) panelOpen = true;
            else panelOpen = false;
        }
    }

}
