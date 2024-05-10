using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                Mission_Manager._MISSION_MANAGER.StartMission();
            }
            if (other.gameObject.CompareTag("MissionObject"))
            {
                UI_Manager._UI_MANAGER.ActivateUIPromptText();
                UI_Manager._UI_MANAGER.UpdateUIPromptText(3);
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                    int idObject = other.GetComponent<Mission_Object_Behaviour>().GetObjectId();
                    Mission_Manager._MISSION_MANAGER.GetMissionObject(idObject);
                    other.GetComponent<Mission_Object_Behaviour>().ObjectGet();
                    Score_Manager._SCORE_MANAGER.ActivateObjectPlus();
                    UI_Manager._UI_MANAGER.ActiveObjectChecker();
                }
            }
            if (other.gameObject.CompareTag("UnderworldGate"))
            {
                //UI_Manager._UI_MANAGER.ActivateUIPromptText();
                //UI_Manager._UI_MANAGER.UpdateUIPromptText(3);
                //if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                //{
                //UI_Manager._UI_MANAGER.DeactivateUIPromptText();
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
                UI_Manager._UI_MANAGER.ActivateScoreTab();
                UI_Manager._UI_MANAGER.StopTimer();
                Score_Manager._SCORE_MANAGER.CheckScore();
                UI_Manager._UI_MANAGER.UpdateScoreTab(Score_Manager._SCORE_MANAGER.UpdateTotalScore());

                //if (SceneManager.GetActiveScene().buildIndex == 3)
                //{
                //    Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.MAINMENU);
                //}
                //Game_Manager._GAME_MANAGER.GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
                //}
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
