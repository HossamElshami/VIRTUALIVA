using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public GameObject MainMenuPanel, FadePanel;
    public GameObject[] pages;
    public DialogBox dialogBox;
    private List<int> usersId;
    private List<User> users;
    public enum dialogType
    {
        Error, Attention, Ask ,Confirmation
    }    

    public static MainManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        usersId = new List<int>();
        users = new List<User>();
    }

    public void BackToPage(GameObject page)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        page.SetActive(true);
    }
    public void showDialogBox(string message, dialogType dialogType)
    {
        dialogBox.gameObject.SetActive(true);
        dialogBox.message.text = message;
        dialogBox.Show(dialogType);
    } 
    public void showDialogBox(string message, dialogType dialogType, float timeToHide)
    {

    }
    public void login()
    {
        if (!PlayerPrefs.HasKey("userCount")) return;
        int _users = PlayerPrefs.GetInt("userCount");
        for (int i = 0; i < _users; i++)
        {
            usersId.Add(i);
            
        }
        foreach (User user in users)
        {
            string[] value = PlayerPrefs.GetString(user.id.ToString()).Split('_');

        }
        for (int i = 0; i < _users; i++)
        {
            
        }
    }
    public void ChooseLab()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
public class User
{
    public int id;
    public string username;
    public string email;
    public string password;
    public string type;

    public void loadData(int id, string username, string email, string password, string type)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
        this.type = type;
    }
}
