using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Web Web;
    public userData userdata;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        Web = GetComponent<Web>();
    }
}
