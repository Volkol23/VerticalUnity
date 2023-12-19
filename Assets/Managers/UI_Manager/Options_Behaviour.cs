using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Behaviour : MonoBehaviour
{
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    private void Awake()
    {
        sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    private void SetMasterVolume(float sliderValue)
    {
        Sound_Manager._SOUND_MANAGER.SetMasterVolume(sliderValue);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    private void SetSFXVolume(float sliderValue)
    {
        Sound_Manager._SOUND_MANAGER.SetSFXVolume(sliderValue);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    private void SetMusicVolume(float sliderValue)
    {
        Sound_Manager._SOUND_MANAGER.SetMusicVolume(sliderValue);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
