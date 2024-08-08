using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField, Tooltip("Set the games master audio mixer.")] private AudioMixer audioMixer;
    [SerializeField, Tooltip ("Select the resolution dropdoown menu located in the canvas > Settings Menu gameobject.")]
    private TMP_Dropdown resolutionDropdown;
    private const string VOLUME = "Volume";
    private Resolution[] resolutions;
    
    private void Start()
    {
        PopulateResolutionSettingsMenu();
    }
    private void PopulateResolutionSettingsMenu() // Add available resolutions to options
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            // Update current resolution to dropdown list
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
        }
        // Populate the resolution list
        resolutionDropdown.AddOptions(options);
        // Set the default dropwdown resultion to match the games resolution
        resolutionDropdown.value = currentResolutionIndex;
        // Display values
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(VOLUME, volume);
    }
    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
