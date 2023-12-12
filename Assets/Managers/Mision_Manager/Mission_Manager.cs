using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    public static Mission_Manager _MISSION_MANAGER;

    private MissionType missionType;

    [SerializeField] private Mission[] missions;
    [SerializeField] private Mission currentMission;

    private bool inDialogue = false;

    private int objectsCounter;

    private int indexDialogue;

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
            indexDialogue = 0;
        }
    }

    private void Update()
    {
        if(missionType != MissionType.NOMISSION && missionType != MissionType.HADES)
        {
            if (objectsCounter == currentMission.GetObjectsNumber())
            {
                currentMission.CompleteObjective();
            }
        }

        if(currentMission != null)
        {
            if (indexDialogue == currentMission.GetIntroDialogue().Length && !currentMission.GetCompleteMission())
            {
                UI_Manager._UI_MANAGER.DeactivateDialogueBox();
                indexDialogue = 0;
                inDialogue = false;
                if (missionType == MissionType.HADES)
                {
                    currentMission.CompleteMission();
                    Debug.Log("HadesCompleta");
                    Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.LEVEL1);
                }
                else
                {
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);
                }
            }
            else if (indexDialogue == currentMission.GetEndDialogue().Length)
            {
                UI_Manager._UI_MANAGER.DeactivateDialogueBox();
                indexDialogue = 0;
                currentMission.CompleteMission();
                if (missionType == MissionType.HADES)
                {
                    Debug.Log("Hades Complete");
                    Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.LEVEL1);
                }
                inDialogue = false;
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);
            }
            if (inDialogue)
            {
                UI_Manager._UI_MANAGER.UpdateDialogueText(currentMission.GetIntroDialogue()[indexDialogue]);
            }
        }
        
    }

    public void StartMission()
    {
        missionType = Game_Manager._GAME_MANAGER.GetMissionLevel();
        SetupMission();   
    }

    public void EndMission()
    {
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
        UI_Manager._UI_MANAGER.ActivateDialogueBox();
        inDialogue = true;
        Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
    }

    public void GetMissionObject(int id)
    {
        UI_Manager._UI_MANAGER.ActivateMissionObjective(currentMission.GetMissionObjectText(id));
        objectsCounter++;
    }

    public void NextDialogue()
    {
        indexDialogue++;
    }

    public bool GetInDialogue()
    {
        return inDialogue;
    }

    public Mission GetCurrentMission()
    {
        return currentMission;
    }
}
