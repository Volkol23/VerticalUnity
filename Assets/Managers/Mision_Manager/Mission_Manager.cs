using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    public static Mission_Manager _MISSION_MANAGER;

    private Vector3 forward;
    private void Awake()
    {
        if(_MISSION_MANAGER != null && _MISSION_MANAGER != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _MISSION_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void StartMission()
    {
        transform.position = new Vector3();


    }
}
