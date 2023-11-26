using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Colliders : MonoBehaviour
{
    private GameGeneral gameGeneral;
    private void OnTriggerStay(Collider other)
    {
        if (gameGeneral == GameGeneral.BOAT)
        {
            if (other.gameObject.CompareTag("ChangePlayerBoat"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);
                }
            }
        }
    }
}
