using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    public Transform parent;
    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("ItemPoint").transform;
    }
    public void makeItem(GameObject item)
    {
        GameObject go = Instantiate(item, parent.position, parent.rotation);
        UI_Manager.instance.openPanel(UI_Manager.instance.inventoryPanel);
        QuestManager.instance.mainObject = go;
    }
}
