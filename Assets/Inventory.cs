using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventoryVisible = false, inRange = false;
    public static Inventory instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Update()
    {
        if (DragingSystem.instance.isDragging) return;
        if (inRange)
        {
            if (InputManager.instance.inputDown(KeyCode.Q))
            {
                if (!inventoryVisible)
                {
                    UI_Manager.instance.openPanel(UI_Manager.instance.inventoryPanel);
                    inventoryVisible = true;
                }
                else
                {
                    UI_Manager.instance.closePanels();
                    inventoryVisible = false;
                }
            }
            UI_Manager.instance.showMessageHint = true;
            if (!InventorySystem.instance.showCellName || InventorySystem.instance.showCellName && !inventoryVisible)
                UI_Manager.instance.ShowHint(inventoryVisible ? "Click [Q] to close the inventory" : "Click [Q] to open the inventory");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            inventoryVisible = false;
            UI_Manager.instance.closePanels();
            UI_Manager.instance.botPanel.SetActive(false);
            UI_Manager.instance.showMessageHint = false;
            InventorySystem.instance.showCellName = false;
        }
    }
}
