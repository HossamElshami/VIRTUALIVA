using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RigisterManager : MonoBehaviour
{
    public TMP_InputField usernameField, emailField, passwordField, confirmPasswordField;
    public ToggleGroup registerType;
    string requiredFieldName;
    private int nextId;

    private static RigisterManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("usersCount")) PlayerPrefs.GetInt("userCount");
        else PlayerPrefs.SetInt("userCount", 0);
        nextId = PlayerPrefs.GetInt("userCount");
    }
    public void register()
    {
        Debug.Log(checkFields());
        if (!checkFields())
        {
            MainManager.instance.showDialogBox("Some fields is required,\n Please check it and try again.", MainManager.dialogType.Attention);
            return;
        }
        else
        {
            if (passwordField.text == confirmPasswordField.text)
            {
                PlayerPrefs.SetString(nextId.ToString(), nextId.ToString() + "_" + usernameField.text + "_" + emailField.text + "_" + passwordField.text + "_" + registerType.ToString());
                MainManager.instance.showDialogBox("Successfuly registered!", MainManager.dialogType.Confirmation);
            }
            else MainManager.instance.showDialogBox("Password and confirm password are not same,\n Please check it and try again.", MainManager.dialogType.Attention);
        }
    }
    bool checkFields()
    {
        if (string.IsNullOrEmpty(usernameField.text) || string.IsNullOrEmpty(emailField.text) || string.IsNullOrEmpty(passwordField.text) || string.IsNullOrEmpty(confirmPasswordField.text)) { 
            //requiredFieldName
            return false; 
        }
        return true;
    }
}
