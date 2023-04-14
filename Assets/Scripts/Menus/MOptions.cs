using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MOptions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown qualityDropdown;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer audioMixer;
    
    [SerializeField] private CanvasGroup principalCanva;
    [SerializeField] private CanvasGroup optionCanva;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipOnClick;

    private void Start()
    {
        
        //Résolution
        resolutionDropdown.ClearOptions();
        
        resolutions = Screen.resolutions;

        System.Array.Reverse(resolutions);

        foreach (Resolution resolution in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(resolution.width + "x" + resolution.height);
            resolutionDropdown.options.Add(option);
        }

        int savedResolutionIndex = PlayerPrefs.GetInt("resolutionIndex", -1);
        if (savedResolutionIndex != -1 && savedResolutionIndex < resolutionDropdown.options.Count)
        {
            resolutionDropdown.value = savedResolutionIndex;
        }
        else
        {
            resolutionDropdown.value = GetResolutionIndex(Screen.currentResolution);
        }

        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        
        //Fullscreen
        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(ChangeFullscreen);
        
        //Qualité
        
        int savedQualityIndex = PlayerPrefs.GetInt("qualityIndex", -1);
        if (savedQualityIndex != -1 && savedQualityIndex < qualityDropdown.options.Count)
        {
            qualityDropdown.value = savedQualityIndex;
        }
        else
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
        }

        qualityDropdown.onValueChanged.AddListener(ChangeQuality);
        
        //Son
        float savedSoundVolume = PlayerPrefs.GetFloat("soundVolume", 0);
        if (savedSoundVolume != 0)
        {
            audioMixer.SetFloat("volume", savedSoundVolume);
            soundSlider.value = savedSoundVolume;
        }
        else
        {
            audioMixer.SetFloat("volume", 0);
            soundSlider.value = 0;
        }
    }

    private int GetResolutionIndex(Resolution resolution)
    {
        for (int i = 0; i < resolutionDropdown.options.Count; i++)
        {
            string[] parts = resolutionDropdown.options[i].text.Split('x');
            int width = int.Parse(parts[0]);
            int height = int.Parse(parts[1]);
            if (resolution.width == width && resolution.height == height)
            {
                return i;
            }
        }
        return 0;
    }

    public void ChangeResolution(int index)
    {
        string[] parts = resolutionDropdown.options[index].text.Split('x');
        int width = int.Parse(parts[0]);
        int height = int.Parse(parts[1]);
        Screen.SetResolution(width, height, Screen.fullScreen);

        PlayerPrefs.SetInt("resolutionIndex", index);
        PlayerPrefs.Save();
    }

    public void ChangeFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        
        PlayerPrefs.SetInt("fullscreen", fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);

        PlayerPrefs.SetInt("qualityIndex", index);
        PlayerPrefs.Save();
    }

    public void BackToPrincipale()
    {
        audioSource.PlayOneShot(audioClipOnClick);
        optionCanva.alpha = 0;
        optionCanva.interactable = false;
        optionCanva.blocksRaycasts = false;
        principalCanva.alpha = 1;
        principalCanva.interactable = true;
        principalCanva.blocksRaycasts = true;
    }

    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("soundVolume", volume);
        audioMixer.SetFloat("volume", volume);
    }
}
