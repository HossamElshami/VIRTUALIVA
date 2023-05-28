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
    public GameObject optionPanel;
    public GameObject editItemOptions;
    public List<Panel> panels;
    [Header("Text")]
    public TMP_Text botText;
    public TMP_Text ItemNameText;
    public GameObject ItemNameContainer;
    public bool panelOpen { get; private set; }
    GameObject openedPanel;
    [Header("Menu")]
    public TMP_Text MenuTitle;
    public TMP_Text MenuDescription;
    public MenuButtonInfoSPO defaultData;

    InputManager inputManager;
    EditItemPage editItemPage;
    Tool draggingObjectTool;

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
        inputManager = InputManager.instance;
        editItemPage = EditItemPage.instance;

        for (int i = 0; i < panels.Count; i++)
            panels[i].gameObject.SetActive(false);
    }
    private void Update()
    {
        if(editItemPage.mainObject) draggingObjectTool = editItemPage.mainObject.GetComponent<Tool>();
        if (!panelOpen && !editItemPage.isEditting) 
            VisibleCursor(false);

        if (inputManager.inputDown(KeyCode.Escape) && panelOpen)
            closePanels();
        else if(inputManager.inputDown(KeyCode.Escape)&& !menu.gameObject.activeInHierarchy)
            openPanel(menu);

        if (DragingSystem.instance.isDragging && inputManager.inputDown(KeyCode.E) && !editItemPage.isEditting)
            optionPanel.SetActive(!optionPanel.activeInHierarchy);
        else if(!DragingSystem.instance.isDragging)
            optionPanel.SetActive(false);

        if (inputManager.inputDown(KeyCode.Q) && !menu.gameObject.activeInHierarchy && !inventoryPanel.gameObject.activeInHierarchy && !DragingSystem.instance.isDragging)
        {
            closePanels();
            openPanel(inventoryPanel);
        }
        else if (inputManager.inputDown(KeyCode.Q) && !menu.gameObject.activeInHierarchy && inventoryPanel.gameObject.activeInHierarchy) closePanels();

        if(editItemOptions.activeInHierarchy && 
        (inputManager.inputDown(KeyCode.T) && draggingObjectTool.toolData.Scaleable |
         inputManager.inputDown(KeyCode.R) && draggingObjectTool.toolData.Rotateable | 
         inputManager.inputDown(KeyCode.U) && draggingObjectTool.toolData.Options | 
         inputManager.inputDown(KeyCode.Y)) && draggingObjectTool.toolData.Material)
        {
            itemOptions(inputManager.GetButton());
        }
    }
    void itemOptions(string msg){
        editItemOptions.SetActive(false);
        botPrint(msg);
    }
    public void botPrint(string text)
    {
        botText.gameObject.SetActive(true);
        botText.text = text;
        closePanels();
        botPanel.SetActive(true);
        openedPanel = botPanel;
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
        VisibleCursor(false);
    }
    void VisibleCursor(bool hide)
    {
        Cursor.lockState = !hide ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = hide;
    }
    public void ShowInformation(MenuButtonInfoSPO data, bool show)
    {
        MenuTitle.text = show ? data.title : defaultData.title;
        MenuDescription.text = show ? data.description : defaultData.description;
    }
}
