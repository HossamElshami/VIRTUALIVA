using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;
    void Start()
    {
        // LoginButton.onClick.AddListener(() =>
        // {
        //   StartCoroutine( Main.Instance.Web.Login(EmailInput.text, PasswordInput.text));
        // });
    }
    public void login()
    {
        StartCoroutine(Main.Instance.Web.Login(EmailInput.text, PasswordInput.text));
    }
}
