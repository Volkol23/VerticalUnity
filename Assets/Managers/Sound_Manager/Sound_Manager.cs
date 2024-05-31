using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager _SOUND_MANAGER;

    [SerializeField]
    private AudioMixer masterMixerReference;

    private AudioMixer masterMixerOriginal;

    private AudioMixerGroup[] mixerGroups;

    [SerializeField]
    private SoundMusicClip[] musicClips;

    [SerializeField]
    private SoundSFXClip[] sfxClips;

    //Enums de todos los audios a precargar
    public enum Music
    {
        defaultTest,
        mainStory,
        mainMenu,
        introScene,
        level1,
        level2,
        level3,
        level3Chase
    }

    public enum SFX
    {
        missionAccomplished,
        newLevelEnter,
        soulAlert,
        soulNotification,
        startCourse,
        pickUpObject,
        trapRound,
        shieldMove,
        congratulations,
        starAppear
    }
    public enum TypeOfSound
    {
        master,
        music,
        sfx
    }

    //Objetos de que contienen los sonidos
    private GameObject oneShotGameObject;
    private GameObject oneShotSfxGameObject;
    private AudioSource oneShotAudioSource;
    private AudioSource oneShotSfxAudioSource;

    private void Awake()
    {
        if(_SOUND_MANAGER != null && _SOUND_MANAGER != this)
        {
            Destroy(_SOUND_MANAGER);
        }
        else
        {
            _SOUND_MANAGER = this;
            DontDestroyOnLoad(_SOUND_MANAGER);

            //Asignar los grupos de mixers del master
            masterMixerOriginal = masterMixerReference;
            mixerGroups = masterMixerOriginal.FindMatchingGroups("Master");

            //Initialize AudioVolume
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0.75f));
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        }
    }

    public void SetMusicVolume(float volumeLevel)
    {
        masterMixerOriginal.SetFloat("MusicVol", Mathf.Log10(volumeLevel) * 20);
    }

    public void SetSFXVolume(float volumeLevel)
    {
        masterMixerOriginal.SetFloat("SFXVol", Mathf.Log10(volumeLevel) * 20);
    }

    public void SetMasterVolume(float volumeLevel)
    {
        masterMixerOriginal.SetFloat("MasterVol", Mathf.Log10(volumeLevel) * 20);
    }

    //Funciones que crean el componente del audio
    public void PlaySFXSound(TypeOfSound type, SFX sfx)
    {
        if (oneShotSfxGameObject == null)
        {
            oneShotSfxGameObject = new GameObject("One Sfx Sound");
            oneShotSfxAudioSource = oneShotSfxGameObject.AddComponent<AudioSource>();
            oneShotSfxAudioSource.outputAudioMixerGroup = GetMixerGroup(type);
        }

        AudioClip sfxClip = GetSFXClip(sfx);
        if (sfxClip != null)
        {
            oneShotSfxAudioSource.clip = sfxClip;
            oneShotSfxAudioSource.Play();
            oneShotSfxAudioSource.loop = false;
        }
        else
        {
            Debug.LogError("SFX clip is null for " + sfx);
        }
    }

    public void PlayMusicSound(TypeOfSound type, Music music)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One Shot Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            oneShotAudioSource.outputAudioMixerGroup = GetMixerGroup(type);
            oneShotAudioSource.loop = true;
        }

        AudioClip musicClip = GetMusicClip(music);
        if (musicClip != null)
        {
            oneShotAudioSource.clip = musicClip;
            oneShotAudioSource.Play();
            oneShotAudioSource.loop = true;
        }
        else
        {
            Debug.LogError("Music clip is null for " + music);
        }
    }

    //Asigna el mixerGroup correnpondiente al clip especifico
    private AudioMixerGroup GetMixerGroup(TypeOfSound type)
    {
        return mixerGroups[(int)type];
    }

    //Buscadores del audio correspondiente
    private AudioClip GetMusicClip(Music music)
    {
        foreach (SoundMusicClip musicClip in musicClips)
        {
            if (musicClip.music == music)
            {
                return musicClip.audioClip;
            }
        }
        Debug.LogError("Sound" + music + " not found");
        return null;
    }
    private AudioClip GetSFXClip(SFX sfx)
    {
        foreach (SoundSFXClip sfxClip in sfxClips)
        {
            if (sfxClip.sfx == sfx)
            {
                return sfxClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sfx + " not found");
        return null;
    }

}
