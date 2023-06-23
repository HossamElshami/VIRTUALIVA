using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] cell[] itemCells;
    [SerializeField] List<InventoryCell> inventoryCells = new List<InventoryCell>();
    [SerializeField] GameObject[] itemsPrefabs;
    [SerializeField] GameObject itemParent;
    [SerializeField] GameObject cellPrefab;
    public ItemSpawnPoint[] spawnPoints;
    [SerializeField] GameObject cellsContainer;
    public GameObject LastInstantiatedTool;
    public bool showCellName = false;
    public static InventorySystem instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        SetUpInventory();
    }
    void SetUpInventory()
    {
        foreach (Transform child in cellsContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < itemsPrefabs.Length; i++)
        {
            GameObject c = Instantiate(cellPrefab, cellsContainer.transform);
            c.GetComponent<InventoryCell>().itemPrefab = itemsPrefabs[i];
            c.transform.GetChild(0).GetComponent<Image>().sprite = itemsPrefabs[i].GetComponent<Tool>().toolData.toolIcon;
            c.GetComponent<InventoryCell>().parent = itemParent.transform;
            inventoryCells.Add(c.GetComponent<InventoryCell>());
        }
    }
}

