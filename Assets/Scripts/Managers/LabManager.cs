using UnityEngine;
using UnityEngine.SceneManagement;

public class LabManager : MonoBehaviour
{
    public GameObject character;
    public bool editingItem = false;
    public GameObject locationEffect;
    public static LabManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        character = FindObjectOfType<Movement>().gameObject;
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
