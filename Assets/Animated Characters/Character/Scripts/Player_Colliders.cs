using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Colliders : MonoBehaviour
{
    private GameGeneral gameGeneral;
    private void Update()
    {
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
    }
    private void OnTriggerStay(Collider other)
    {
        if(gameGeneral == GameGeneral.PLAYER)
        {
            if (other.gameObject.CompareTag("ChangeBoatPlayer"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
                }
            }
            if(other.gameObject.CompareTag("MissionObject"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    Mission_Manager._MISSION_MANAGER.GetMissionObject();
                }
            }
        }
    }
}
