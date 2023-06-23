using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField EmailInput;
    public InputField PasswordInput;
    public InputField UsertypeInput;
    public Button RegisterButton;
    void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.Register(UsernameInput.text, EmailInput.text, PasswordInput.text, UsertypeInput.text));
        });

    }
}
