using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour {

    #region Setup

    public static OptionsManager Instance;

    private void Awake() {
        Instance = this;
        Debug.Log("optionsmanager");
        musicLevel = -80f;
        masterLevel = 0f;
        _mainAudioMixer.SetFloat("musicVolume", musicLevel);
        _mainAudioMixer.SetFloat("masterVolume", masterLevel);
    }

    #endregion

    #region Audio

    [HideInInspector] public float musicLevel, masterLevel;

    public AudioMixer _mainAudioMixer;

    public void OnMasterChange(float operation) {
        masterLevel = OnAudioChange(masterLevel, operation);
        _mainAudioMixer.SetFloat("masterVolume", masterLevel);
    }

    public void OnMusicChange(float operation) {
        musicLevel = OnAudioChange(musicLevel, operation);
        _mainAudioMixer.SetFloat("musicVolume", musicLevel);      
    }

    private float OnAudioChange(float audioLevel, float operation) {
        if (operation == 1) {
            switch (audioLevel) {
                case 0f:
                    audioLevel = -80f;
                    break;

                case -80f:
                    audioLevel = -27f;
                    break;

                default:
                    audioLevel = audioLevel + (operation * 3f);
                    break;
            }
        }
        else switch (audioLevel) {
                case -27f:
                    audioLevel = -80f;
                    break;

                case -80f:
                    audioLevel = 0f;
                    break;

                default:
                    audioLevel = audioLevel + (operation * 3f);
                    break;
            }
        return audioLevel;
    }

    #endregion

    #region Controls

    //Later Update

    #endregion

    #region Video

    public void OnResolutionChange() {

    }

    public void OnFullscreenChange() {

    }

    #endregion

    #region Graphics

    public int[] framerateSettings = {
        30,
        60,
        90,
        120,
        150,
        180,
        210,
        240,
    };
    
    public int framerateSettingsIndex = 5;

    public string[] qualitySettings = {
        "Very Low",
        "Low",
        "Medium",
        "High",
        "Very High",
        "Ultra"
    };

    public int qualitySettingsIndex = 5;

    public void OnQualityChange(float operation) {
        if(operation == 1) {
            if (qualitySettingsIndex == qualitySettings.Length - 1) {
                qualitySettingsIndex = 0;
            }
            else qualitySettingsIndex++;
        }
        else if(operation == -1) { 
            if(qualitySettingsIndex == 0) {
                qualitySettingsIndex = qualitySettings.Length - 1;
            }
            else qualitySettingsIndex--;
        }
    }
    
    public void OnApplyQuality() {
        QualitySettings.SetQualityLevel(qualitySettingsIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void OnFramerateChange(float operation) {
        if (operation == 1) {
            if (framerateSettingsIndex == framerateSettings.Length - 1) {
                framerateSettingsIndex = 0;
            }
            else framerateSettingsIndex++;
        }
        else if (operation == -1) {
            if (framerateSettingsIndex == 0) {
                framerateSettingsIndex = framerateSettings.Length - 1;
            }
            else framerateSettingsIndex--;
        }
    }

    public void OnApplyFramerate() {
        Application.targetFrameRate = framerateSettings[framerateSettingsIndex];
    }

    #endregion

}
