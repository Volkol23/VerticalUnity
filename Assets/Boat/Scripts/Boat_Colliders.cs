using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Colliders : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChangePlayerBoat"))
        {
            Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);
        }
    }
}
