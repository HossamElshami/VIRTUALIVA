using UnityEngine;
using UnityEngine.UI;

public class cell : MonoBehaviour
{
    public string name;
    public GameObject image;
    public GameObject prefab;
    void Start()
    {
        if (prefab)
            name = prefab.name;
        image = transform.GetChild(0).gameObject;
        image.GetComponent<Image>().enabled = false;
    }
}
