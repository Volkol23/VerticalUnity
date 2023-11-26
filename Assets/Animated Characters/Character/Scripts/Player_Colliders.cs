using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Colliders : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
        {
            Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
        }
    }
}
