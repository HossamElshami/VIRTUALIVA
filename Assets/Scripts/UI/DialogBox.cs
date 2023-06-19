using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
    public TMP_Text message;
    [SerializeField]
    private GameObject errorBtn, attentionBtn, askBtn, confirmationBtn, LoadingIcon;
    [SerializeField]
    private Image boxIcon;
    [SerializeField]
    private Sprite errorIcon, attentionIcon, askIcon, confirmationIcon;
    [SerializeField]
    private Color errorColor, attentionColor, askColor, confirmationColor;

    public static DialogBox instance;
    private void Awake()
    {
        instance = this;
    }
    public void CloseApp()
    {
        Application.Quit();
    }
    public void Show(MainManager.dialogType dialogType, string msg)
    {
        LoadingIcon.SetActive(false);
        boxIcon.enabled = true;
        switch (dialogType)
        {
            case MainManager.dialogType.Error:
                errorBtn.SetActive(true);
                boxIcon.sprite = errorIcon;
                boxIcon.color = errorColor;
                AudioManager.instance.Play("Error");
                break;
            case MainManager.dialogType.Attention:
                attentionBtn.SetActive(true);
                boxIcon.sprite = attentionIcon;
                boxIcon.color = attentionColor;
                AudioManager.instance.Play("Warning");
                break;
            case MainManager.dialogType.Ask:
                askBtn.SetActive(true);
                boxIcon.sprite = askIcon;
                boxIcon.color = askColor;
                break;
            case MainManager.dialogType.Confirmation:
                confirmationBtn.SetActive(true);
                boxIcon.sprite = confirmationIcon;
                boxIcon.color = confirmationColor;
                break;
            case MainManager.dialogType.Loading:
                boxIcon.enabled = false;
                LoadingIcon.SetActive(true);
                confirmationBtn.SetActive(false);
                errorBtn.SetActive(false);
                attentionBtn.SetActive(false);
                break;
        }
        message.text = msg;
    }
}
