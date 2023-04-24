using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("UI")]
    public GameObject Fade;
    public DialogBox dialogBox;
    public GameObject[] pages;

    public static Manager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (!MainManager.instance) return;
        MainManager.instance.pages.Clear();
        MainManager.instance.FadePanel = Fade;
        MainManager.instance.dialogBox = dialogBox;
        for (int i = 0; i < pages.Length; i++)
        {
            MainManager.instance.pages.Add(pages[i]);
        }
    }
    public void GoToScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void GoToPage(GameObject page)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        page.SetActive(true);
    }
    public void ChooseLab()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
