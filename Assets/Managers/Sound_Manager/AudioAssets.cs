using UnityEngine;

public class SoundClip
{
    public Sound_Manager.TypeOfSound type;
}

[System.Serializable]
public class SoundMusicClip : SoundClip
{
    public Sound_Manager.Music music;
    public AudioClip audioClip;
}
[System.Serializable]
public class SoundSFXClip : SoundClip
{
    public Sound_Manager.SFX sfx;
    public AudioClip audioClip;
}
