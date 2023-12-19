using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Colliders : MonoBehaviour
{
    private GameGeneral gameGeneral;

    private void Update()
    {
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
    }
    private void OnTriggerStay(Collider other)
    {
        if (gameGeneral == GameGeneral.BOAT)
        {
            if (other.gameObject.CompareTag("ChangePlayerBoat"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    Transform newPosition;
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    newPosition = player.GetComponent<Interactive_Objects>().GetClosestDock();

                    UI_Manager._UI_MANAGER.ActivateAbilities();
                    player.GetComponent<Character_Behaviour>().SetDockPosition(newPosition);
                }
            }
            if (other.gameObject.CompareTag("MissionTrigger"))
            {
                Mission_Manager._MISSION_MANAGER.StartMission();
            }
        }
    }
}
