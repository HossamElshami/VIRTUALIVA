using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour
{
    [Header("Panels")]
    public Panel menu;
    public Panel questPanel;
    public Panel inventoryPanel;
    public GameObject botPanel;
    public GameObject editPanel;
    public GameObject optionPanel;
    public GameObject dragingPanel;
    public GameObject itemPanel;
    public List<Panel> panels;
    //public GameObject _quest;
    [Header("Text")]
    public TMP_Text botText;
    public TMP_Text itemNameText;
    public TMP_Text hoverItemNameText;
    public bool panelOpen { get; private set; }
    [SerializeField]
    GameObject openedPanel;
    [Header("Menu")]
    public TMP_Text MenuTitle;
    public TMP_Text MenuDescription;
    public MenuButtonInfoSPO defaultData;

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
        else if (InputManager.instance.inputDown(KeyCode.Escape) && menu.gameObject.activeInHierarchy)
        {
            closePanels();
        }
        if (DragingSystem.instance.isDragging && InputManager.instance.inputDown(KeyCode.E))
            optionPanel.SetActive(!optionPanel.activeInHierarchy);
        else if(!DragingSystem.instance.isDragging)
            optionPanel.SetActive(false);
        //hoverItemNameText.gameObject.SetActive(!optionPanel.activeInHierarchy);
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
        openedPanel = botPanel;
        Invoke(nameof(closePanelDelay), 3f);
    }
    void closePanelDelay()
    {
        if (!openedPanel) return;
        openedPanel.SetActive(false);
        openedPanel = null;
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
        openedPanel = panel.gameObject;
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
    public void ShowInformation(MenuButtonInfoSPO data, bool show)
    {
        if (show)
        {
            MenuTitle.text = data.title;
            MenuDescription.text = data.description;
        }
        else
        {
            MenuTitle.text = defaultData.title;
            MenuDescription.text = defaultData.description;
        }
    }
}
