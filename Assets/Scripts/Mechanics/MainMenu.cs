using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioMixer masterMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    private Resolution[] availableResolutions;

    // Retrieve available resolutions
    private void Awake()
    {
        //Set quality to default which is high, and default volume
        SetQuality(qualityDropdown.value);
        SetVolume(volumeSlider.value);
        
        availableResolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        // int currentResIndex = 0;
        // for (int i = 0; i < availableResolutions.Length; i++)
        // {
        //     string currentOption = availableResolutions[i].width + "x" + availableResolutions[i].height;
        //     resolutionOptions.Add(currentOption);
        //
        //     if (availableResolutions[i].width == Screen.currentResolution.width && availableResolutions[i].height == Screen.currentResolution.height)
        //     {
        //         currentResIndex = i;
        //     }
        // }
        // // Takes in a list of strings so must reformat the array into nicely formatted strings
        // resolutionDropdown.AddOptions(resolutionOptions);
        // resolutionDropdown.value = currentResIndex;
        // resolutionDropdown.RefreshShownValue();
        SetResolution();
    }
    public void PlayGame()
    {
        GameMasterController.isReplay = false;
        Debug.Log("Playing game:" + (SceneManager.GetActiveScene().buildIndex + 1));
        Debug.Log("REPLAY IS PLAYING?? " + GameMasterController.isReplay.ToString());
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Game");
    }

    public void Replay()
    {
        GameMasterController.isReplay = true;
        Debug.Log("REPLAY PLAYING" + GameMasterController.isReplay.ToString());
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
    
    // Options section
    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("masterVolume", volume);
        Debug.Log(volume);
    }

    public void SetQuality(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex);
        Debug.Log("Quality is now " + graphicIndex);
    }

    public bool SetFullScreen(bool isFullScreen)
    {
        return !Screen.fullScreen;
    }

    public void SetResolution()
    {
        // Resolution res = availableResolutions[resolutionIndex];
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        // Debug.Log("Screen resolution set to: " + res.width + "x" + res.height + " and Fullscreen is set to " + Screen.fullScreen);
    }
}
