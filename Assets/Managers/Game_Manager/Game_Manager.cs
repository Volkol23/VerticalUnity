using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager _GAME_MANAGER;

    private GameGeneral currentGeneral;

    private MissionType missionLevel;

    private void Awake()
    {
        if(_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _GAME_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }

        currentGeneral = GameGeneral.PLAYER;
        missionLevel = MissionType.ARACNHE;
    }

    public void ChangeGeneral(GameGeneral gameGeneralState)
    {
        currentGeneral = gameGeneralState;
        if(currentGeneral == GameGeneral.PLAYER)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = player.GetComponent<InteractiveObjects>().GetClosestDock().position;
        }
        if(currentGeneral == GameGeneral.BOAT)
        {
            GameObject boat = GameObject.FindGameObjectWithTag("Boat");
            boat.GetComponent<Movement>().SetDockPosition();
        }
    }

    public GameGeneral GetCurrentGeneral()
    {
        return currentGeneral;
    }

    public MissionType GetMissionLevel()
    {
        return missionLevel;
    }
    
}
