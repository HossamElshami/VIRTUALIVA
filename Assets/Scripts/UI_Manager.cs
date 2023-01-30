using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour
{
    [Header("Panels")]
    public Panel menu;
    public Panel questPanel;
    public Panel inventoryPanel;
    public GameObject botPanel;
    public GameObject editPanel;
    public GameObject itemPanel;
    public List<Panel> panels;
    //public GameObject _quest;
    [Header("Text")]
    public TMP_Text botText;
    public TMP_Text itemNameText;
    public bool panelOpen { get; private set; }
    [SerializeField]
    Panel openedPanel;

    public static UI_Manager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);        
    }
    private void Start()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (InputManager.instance.inputDown(KeyCode.Escape) && !menu.gameObject.activeInHierarchy)
        {
            openPanel(menu);
        }
        else if (InputManager.instance.inputDown(KeyCode.Escape) && menu.gameObject.activeInHierarchy             )
        {
            closePanels();
        }

        if (!panelOpen) VisibleCursor(false);
        //if (InputManager.instance.inputDown(KeyCode.Q)) openPanel(inventoryPanel, !inventoryPanel.gameObject.activeInHierarchy);
        //if (openedPanel && openedPanel.gameObject.activeInHierarchy) VisibleCursor(openedPanel.HideCursor);
        //else VisibleCursor(true);
    }
    public void botPrint(string text)
    {
        botText.gameObject.SetActive(true);
        botText.text = text;
        closePanels();
        botPanel.SetActive(true);
        Invoke("closePanelDelay(botPanel)", 3f);
    }
    void closePanelDelay(GameObject panel)
    {
        panel.SetActive(false);
    }
    void AddQuestToPanel()
    {
        openPanel(questPanel);
    }
    void itemProperties()
    {
        closePanels();
        itemPanel.SetActive(true);
    }
    public void openPanel(Panel panel)
    {
        closePanels();
        panel.gameObject.SetActive(true);
        openedPanel = panel;
        panelOpen = true;
        VisibleCursor(panel.VisibleCursor);
    }
    public void closePanels()
    {
        if (!panelOpen) return;
        for (int i = 0; i < panels.Count; i++)
        {
            panelOpen = false;
            panels[i].gameObject.SetActive(false);
        }
    }
    void VisibleCursor(bool hide)
    {
        Cursor.lockState = !hide ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = hide;
    }
}
