using UnityEngine;

public class LiquidMixer : MonoBehaviour
{
    public GameObject firstLiquid, secondLiquid;
    [SerializeField] MixerPoint point1, point2;
    public bool firstLiquidFill = false, secondLiquidFill = false;
    public Transform spawnPoint;
    public bool inRange = false;
    [SerializeField] GameObject[] chemicals;
    public GameObject outputLiquid;

    public static LiquidMixer instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (DragingSystem.instance)
        {
            if (DragingSystem.instance.isDragging) return;
        }
        if (inRange)
        {
            if (InputManager.instance.inputDown(KeyCode.Q))
            {
                MixLiquid();
            }
            UI_Manager.instance.showMessageHint = true;
            if (!InventorySystem.instance.showCellName || InventorySystem.instance.showCellName)
                UI_Manager.instance.ShowHint("Click [Q] to mix liquids");

        }
        firstLiquidFill = point1.detectLiquid;
        secondLiquidFill = point2.detectLiquid;
        firstLiquid = point1.chemical;
        secondLiquid = point2.chemical;
    }
    public void MixLiquid()
    {
        if (!firstLiquidFill || !secondLiquidFill || !firstLiquid || !secondLiquid)
        {
            UI_Manager.instance.botPrint("Please put liquids first and try again.", 3f);
            return;
        }

        if (!GetTheMixerChemical(firstLiquid, secondLiquid)) return;

        outputLiquid = Instantiate(GetTheMixerChemical(firstLiquid, secondLiquid), spawnPoint.position, spawnPoint.rotation);
        resetLiquid();
    }
    GameObject GetTheMixerChemical(GameObject c1, GameObject c2)
    {
        for (int i = 0; i < chemicals.Length; i++)
        {
            if (chemicals[i].GetComponent<Chemical>().data.Chemicals1 || chemicals[i].GetComponent<Chemical>().data.Chemicals2)
            {
                if (c1.GetComponent<Chemical>().data == chemicals[i].GetComponent<Chemical>().data.Chemicals1 &&
                 c2.GetComponent<Chemical>().data == chemicals[i].GetComponent<Chemical>().data.Chemicals2 ||
                 c2.GetComponent<Chemical>().data == chemicals[i].GetComponent<Chemical>().data.Chemicals1 &&
                 c1.GetComponent<Chemical>().data == chemicals[i].GetComponent<Chemical>().data.Chemicals2) return chemicals[i];
            }
        }
        return null;
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
            UI_Manager.instance.closePanels();
            UI_Manager.instance.botPanel.SetActive(false);
            UI_Manager.instance.showMessageHint = false;
            InventorySystem.instance.showCellName = false;
        }
    }
    void resetLiquid()
    {
        Destroy(firstLiquid);
        Destroy(secondLiquid);
        UI_Manager.instance.closePanels();
        firstLiquid = secondLiquid = point1.chemical = point2.chemical = null;
        firstLiquidFill = secondLiquidFill = point1.detectLiquid = point2.detectLiquid = false;
    }
}
