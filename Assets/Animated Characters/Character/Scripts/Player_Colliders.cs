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
                    int idObject = other.GetComponent<MissionObjectBehaviour>().GetObjectId();
                    Mission_Manager._MISSION_MANAGER.GetMissionObject(idObject);
                    other.GetComponent<MissionObjectBehaviour>().ObjectGet();
                    characterAnimator.PickUpAnimation();
                }
            }
            if (other.gameObject.CompareTag("MissionCharacter"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    Mission_Manager._MISSION_MANAGER.InitMission();
                    characterAnimator.BowAnimation();
                }
            }
            if (other.gameObject.CompareTag("UnderworldGate"))
            {
                if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
                {
                    if(SceneManager.GetActiveScene().buildIndex == 3)
                    {
                        Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.MAINMENU);
                    }

                    Game_Manager._GAME_MANAGER.GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
