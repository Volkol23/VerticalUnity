using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    public static Mission_Manager _MISSION_MANAGER;

    private MissionType missionType;

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

    private void Update()
    {
        switch (missionType)
        {
            case MissionType.ARACNHE:
                SetupArachne();
                break;
        }
    }
    public void StartMission()
    {
        missionType = MissionType.ARACNHE;
    }

    public void EndMission()
    {
        missionType = MissionType.NOMISSION;
    }

    private void SetupArachne()
    {

    }
}
