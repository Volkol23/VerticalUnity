using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Colliders : MonoBehaviour
{
    private GameGeneral gameGeneral;

    private Character_Animator_Behaviour characterAnimator;
    private void Update()
    {
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
    }

    private void Awake()
    {
        characterAnimator = GetComponent<Character_Animator_Behaviour>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(gameGeneral == GameGeneral.PLAYER)
        {
            UI_Manager._UI_MANAGER.ActivateUIPromptText();

            if (other.gameObject.CompareTag("ChangeBoatPlayer"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                    Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
                }
            }
            if(other.gameObject.CompareTag("MissionObject"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                    int idObject = other.GetComponent<Mission_Object_Behaviour>().GetObjectId();
                    Mission_Manager._MISSION_MANAGER.GetMissionObject(idObject);
                    other.GetComponent<Mission_Object_Behaviour>().ObjectGet();
                    characterAnimator.PickUpAnimation();
                }
            }
            if (other.gameObject.CompareTag("MissionCharacter"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                    Mission_Manager._MISSION_MANAGER.InitMission();
                    characterAnimator.BowAnimation();
                }
            }
            if (other.gameObject.CompareTag("UnderworldGate"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    UI_Manager._UI_MANAGER.DeactivateUIPromptText();

                    if (SceneManager.GetActiveScene().buildIndex == 3)
                    {
                        Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.MAINMENU);
                    }

                    Game_Manager._GAME_MANAGER.GoToScene(SceneManager.GetActiveScene().buildIndex + 1);

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameGeneral == GameGeneral.PLAYER)
        {
            if (other.gameObject.CompareTag("ChangeBoatPlayer"))
            {
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
            }
            if (other.gameObject.CompareTag("UnderworldGate"))
            {
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
            }
            if (other.gameObject.CompareTag("MissionCharacter"))
            {
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
            }
            if (other.gameObject.CompareTag("MissionObject"))
            {
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
            }
        }
    }
}
