using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
    public TMP_Text message;
    [SerializeField]
    private GameObject errorBtn, attentionBtn, askBtn, confirmationBtn;
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
    public void Show(MainManager.dialogType dialogType)
    {
        switch (dialogType)
        {
            case MainManager.dialogType.Error:
                errorBtn.SetActive(true);
                boxIcon.sprite = errorIcon;
                boxIcon.color = errorColor;
                break;
            case MainManager.dialogType.Attention:
                attentionBtn.SetActive(true);
                boxIcon.sprite = attentionIcon;
                boxIcon.color = attentionColor;
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
        }
    }
}
