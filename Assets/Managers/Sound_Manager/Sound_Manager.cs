using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager _SOUND_MANAGER;

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
        }
    }
}
