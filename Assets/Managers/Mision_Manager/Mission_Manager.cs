using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    public static Mission_Manager _MISSION_MANAGER;

    private MissionType missionType;

    [SerializeField] private Mission[] missions;
    [SerializeField] private Mission currentMission;

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
            missionType = MissionType.NOMISSION;
        }
    }

    private void Update()
    {

    }

    public void StartMission()
    {
        missionType = Game_Manager._GAME_MANAGER.GetMissionLevel();
        SetupMission();       
    }

    public void EndMission()
    {
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionCompletedText());
        missionType = MissionType.NOMISSION;
        currentMission = null;
    }

    private void SetupMission()
    {
        foreach (Mission mission in missions)
        {
            if (mission.GetMissionType() == missionType)
            {
                currentMission = mission;
            }
        }
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionObjectiveText());
    }

    public void GetMissionObject()
    {
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionObjectText());
    }
}
