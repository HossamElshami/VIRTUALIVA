using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;
    public AudioMixer MainMixer;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value =   currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    public void MainAudioVolume(float volume)
    {
        MainMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    } 
    public void SetFullScreen(bool IsFullScreen)
    {        
        Screen.fullScreen = IsFullScreen;
    }   
    public void SetMute(bool IsMute)
    {
        AudioListener.volume = IsMute ? 0 : 1;
    }
}
