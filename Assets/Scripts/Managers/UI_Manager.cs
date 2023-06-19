using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour
{
    [Header("Panels")]
    public List<Panel> panels;
    public Panel menu, questPanel, inventoryPanel;
    public GameObject botPanel, optionPanel, editItemOptions;
    [Header("Text")]
    public TMP_Text botText, ItemNameText;
    public GameObject ItemNameContainer;
    public bool panelOpen { get; private set; }
    public bool showMessageHint = false;
    GameObject openedPanel;
    [Header("Menu")]
    public TMP_Text MenuTitle, MenuDescription;
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
        if (editItemPage.mainObject) draggingObjectTool = editItemPage.mainObject.GetComponent<Tool>();
        if (!panelOpen && !editItemPage.isEditting)
            VisibleCursor(false);

        if (inputManager.inputDown(KeyCode.Escape) && panelOpen)
            closePanels();
        else if (inputManager.inputDown(KeyCode.Escape) && !menu.gameObject.activeInHierarchy)
            openPanel(menu);

        if (DragingSystem.instance.isDragging && inputManager.inputDown(KeyCode.E) && !editItemPage.isEditting)
            optionPanel.SetActive(!optionPanel.activeInHierarchy);
        else if (!DragingSystem.instance.isDragging)
            optionPanel.SetActive(false);

        if (editItemOptions.activeInHierarchy &&
        (inputManager.inputDown(KeyCode.T) && draggingObjectTool.toolData.Scaleable |
         inputManager.inputDown(KeyCode.R) && draggingObjectTool.toolData.Rotateable |
         inputManager.inputDown(KeyCode.U) && draggingObjectTool.toolData.Options |
         inputManager.inputDown(KeyCode.Y)) && draggingObjectTool.toolData.Material)
        {
            itemOptions(inputManager.GetButton());
        }
    }
    void itemOptions(string msg)
    {
        editItemOptions.SetActive(false);
        botPrint(msg);
    }
    public void botPrint(string text)
    {
        botText.gameObject.SetActive(true);
        botText.text = text;
        botPanel.SetActive(true);
        openedPanel = botPanel;
    }
    public void ShowHint(string text)
    {
        if (!showMessageHint)
        {
            HideHint();
            return;
        }
        ItemNameContainer.SetActive(true);
        ItemNameText.text = text;
    }
    public void ShowHint(InventoryCell cell)
    {
        if (!showMessageHint)
        {
            HideHint();
            return;
        }
        ItemNameContainer.SetActive(true);
        ItemNameText.text = cell.itemPrefab.GetComponent<Tool>().toolData.toolName;
    }
    public void HideHint()
    {
        ItemNameContainer.SetActive(false);
        ItemNameText.text = "";
    }
    void closePanelDelay()
    {
        if (!openedPanel) return;
        openedPanel.SetActive(false);
        openedPanel = null;
    }
    void AddQuestToPanel()
    {
        closePanels();
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
