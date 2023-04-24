using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void ShowMainMenu()
    {
        MainManager.instance.FadePanel.SetActive(true);
        MainManager.instance.MainMenuPanel.SetActive(true);
        Camera.main.backgroundColor = Color.black;
    }
}
