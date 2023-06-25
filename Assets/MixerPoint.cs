using UnityEngine;

public class MixerPoint : MonoBehaviour
{
    public bool detectLiquid = false;
    public GameObject chemical;
    [SerializeField] GameObject effect;
    void Update()
    {
        effect.SetActive(!detectLiquid);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Tool>().toolData.toolCategory == "Liquid" )
        {
            detectLiquid = true;
            chemical = other.gameObject;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Tool>().toolData.toolCategory == "Liquid" )
        {
            detectLiquid = true;
            chemical = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Tool>().toolData.toolCategory == "Liquid")
        {
            detectLiquid = false;
            chemical = null;
        }
    }
}
