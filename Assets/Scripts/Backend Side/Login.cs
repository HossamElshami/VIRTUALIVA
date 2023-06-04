using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;
    public void login()
    {
        if (EmailInput.text != string.Empty && PasswordInput.text != string.Empty)
        {
            StartCoroutine(Main.Instance.Web.Login(EmailInput.text, PasswordInput.text));
        }
        else if (EmailInput.text == string.Empty || PasswordInput.text == string.Empty)
        {
            MainManager.instance.showDialogBox("Please enter your email and password and try again.", MainManager.dialogType.Attention);
        }
    }
}
