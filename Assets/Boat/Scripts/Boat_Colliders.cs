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
                    Vector3 newPosition;
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    newPosition = player.GetComponent<InteractiveObjects>().GetClosestDock().position;

                    player.GetComponent<Character_Behaviour>().SetDockPosition(newPosition);
                }
            }
        }
    }
}
