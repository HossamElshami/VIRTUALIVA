using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemPrefab;
    public Transform parent;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { makeItem(itemPrefab); });
    }
    public void makeItem(GameObject item)
    {
        GameObject go = Instantiate(item, GetFreeSpawnPoint().position, GetFreeSpawnPoint().rotation);
        UI_Manager.instance.openPanel(UI_Manager.instance.inventoryPanel);
        QuestManager.instance.mainObject = go;
        InventorySystem.instance.LastInstantiatedTool = go;
    }
    Transform GetFreeSpawnPoint()
    {
        foreach (ItemSpawnPoint p in InventorySystem.instance.spawnPoints)
            if (p.isFree) return p.transform;
        return null;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_Manager.instance.ShowHint(this);
        InventorySystem.instance.showCellName = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Manager.instance.HideHint(); ;
        InventorySystem.instance.showCellName = false;
    }
}
