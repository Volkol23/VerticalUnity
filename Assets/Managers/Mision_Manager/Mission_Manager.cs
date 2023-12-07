using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    public static Mission_Manager _MISSION_MANAGER;

    private MissionType missionType;

    [SerializeField] private Mission[] missions;
    [SerializeField] private Mission currentMission;

    private int objectsCounter;

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
            objectsCounter = 0;
        }
    }

    private void Update()
    {
        if(missionType != MissionType.NOMISSION)
        {
            if (objectsCounter == currentMission.GetObjectsNumber())
            {
                currentMission.CompleteObjective();
            }
        }
    }

    public void StartMission()
    {
        missionType = Game_Manager._GAME_MANAGER.GetMissionLevel();
        SetupMission();
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionStartText());      
    }

    public void EndMission()
    {
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionCompletedText());
        missionType = MissionType.NOMISSION;
        currentMission = null;
        objectsCounter = 0;
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
    }

    public void InitMission()
    {
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionObjectiveText());
    }

    public void GetMissionObject(int id)
    {
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionObjectText(id));
        objectsCounter++;
    }
}
