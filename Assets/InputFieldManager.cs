using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    Color FocusColor = Color.white, OutFocusColor = Color.gray;
    [SerializeField] Image underLine;
    [SerializeField] TMP_Text text;
    [SerializeField] bool OnFocus = false;
    void Start()
    {
        underLine = transform.Find("UnderLine").GetComponent<Image>();
        text = transform.GetChild(0).Find("Text").GetComponent<TMP_Text>();
        text.text = "";
    }
    void LateUpdate()
    {
        if (OnFocus) return;

        if (text.text.Count() > 1)
            underLine.color = FocusColor;
        else
            underLine.color = OutFocusColor;
    }
    public void FocusInputField()
    {
        underLine.color = FocusColor;
        OnFocus = true;
    }
    public void OutFocusInputField()
    {
        underLine.color = OutFocusColor;
        OnFocus = false;
    }
    public void OnFieldEnterValue()
    {
        AudioManager.instance.Play("ButtonClick");
    }
}
