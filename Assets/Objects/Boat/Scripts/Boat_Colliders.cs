using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Colliders : MonoBehaviour
{
    private GameGeneral gameGeneral;

    private bool colliding;
    private void Update()
    {
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
    }
    private void OnTriggerStay(Collider other)
    {
        if (gameGeneral == GameGeneral.BOAT)
        {
            //UI_Manager._UI_MANAGER.ActivateUIPromptText();

            if (other.gameObject.CompareTag("ChangePlayerBoat"))
            {
                UI_Manager._UI_MANAGER.UpdateUIPromptText(1);
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                    Transform newPosition;
                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.PLAYER);

                    GameObject boat = GameObject.FindGameObjectWithTag("Boat");
                    GameObject player = GameObject.FindGameObjectWithTag("Player");

                    newPosition = player.GetComponent<Interactive_Objects>().GetClosestDock().GetComponent<Dock_Save_Point>().GetDockPositon();
                    player.GetComponent<Character_Behaviour>().SetDockPosition(newPosition);

                    newPosition = player.GetComponent<Interactive_Objects>().GetClosestDock().GetComponent<Dock_Save_Point>().GetBoatPosition();
                    boat.GetComponent<Movement>().SetDockPosition(newPosition);
                }
            }
            if (other.gameObject.CompareTag("MissionTrigger"))
            {
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                Mission_Manager._MISSION_MANAGER.StartMission();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameGeneral == GameGeneral.BOAT)
        {
            if (other.gameObject.CompareTag("ChangePlayerBoat"))
            {
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameGeneral == GameGeneral.BOAT)
        {
            if(collision.gameObject.layer == 0)
            {
                colliding = true;
            }

            if (collision.gameObject.CompareTag("Hazard"))
            {
                Debug.Log("DamageTick");
                float damage = collision.gameObject.GetComponent<HazardBehaviour>().GetDamageTick();
                Game_Manager._GAME_MANAGER.GetDamage(damage);
            }

            if (collision.gameObject.CompareTag("DeathTrap"))
            {
                Debug.Log("DamageLarge");
                float damage = collision.gameObject.GetComponent<HazardBehaviour>().GetDamageTick();
                Game_Manager._GAME_MANAGER.GetDamage(damage);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameGeneral == GameGeneral.BOAT)
        {
            if (collision.gameObject.layer == 0)
            {
                colliding = false;
            }
        }
    }

    public bool GetColliding()
    {
        return colliding; 
    }
}
