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
    private Resolution[] availableResolutions;

    // Retrieve available resolutions
    void Start()
    {
        //Set quality to default which is high
        SetQuality(qualityDropdown.value);
        availableResolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string currentOption = availableResolutions[i].width + "x" + availableResolutions[i].height;
            resolutionOptions.Add(currentOption);

            if (availableResolutions[i].width == Screen.currentResolution.width && availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        // Takes in a list of strings so must reformat the array into nicely formatted strings
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();


    }
    public void PlayGame()
    {
        Debug.Log("Playing game:" + (SceneManager.GetActiveScene().buildIndex + 1));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        Debug.Log("Quality is now" + graphicIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = availableResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
