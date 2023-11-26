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
        Debug.Log("TriggerPlyaer");
        if(gameGeneral == GameGeneral.PLAYER)
        {
            if (other.gameObject.CompareTag("ChangePlayerBoat"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    Debug.Log("Input");
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
                }
            }
        }
    }
}
