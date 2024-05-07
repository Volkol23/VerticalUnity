using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Collider : MonoBehaviour
{
    private GameGeneral gameGeneral;

    [SerializeField]
    private bool startTrigger = true;
    [SerializeField]
    private bool objectTrigger = true;
    [SerializeField]
    private bool warnTrigger = true;
    private void Update()
    {
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StartSoul"))
        {
            //ID texto de alma
            if (startTrigger)
            {
                UI_Manager._UI_MANAGER.ActivateUIPromptText();
                UI_Manager._UI_MANAGER.StopTimer();
                Sound_Manager._SOUND_MANAGER.PlaySFXSound(Sound_Manager.TypeOfSound.sfx, Sound_Manager.SFX.soulNotification);
                UI_Manager._UI_MANAGER.UpdateUIPromptText(0);
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
                startTrigger = false;
            }            
        }
        if (other.gameObject.CompareTag("ObjectSoul"))
        {
            //ID texto de alma
            if (objectTrigger)
            {
                UI_Manager._UI_MANAGER.ActivateUIPromptText();
                UI_Manager._UI_MANAGER.StopTimer();
                Sound_Manager._SOUND_MANAGER.PlaySFXSound(Sound_Manager.TypeOfSound.sfx, Sound_Manager.SFX.soulNotification);
                UI_Manager._UI_MANAGER.UpdateUIPromptText(1);
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
                Debug.Log("StartTrigger");
                objectTrigger = false;
            }
        }
        if (other.gameObject.CompareTag("WarnSoul"))
        {
            //ID texto de alma
            if (warnTrigger)
            {
                UI_Manager._UI_MANAGER.ActivateUIPromptText();
                UI_Manager._UI_MANAGER.StopTimer();
                Sound_Manager._SOUND_MANAGER.PlaySFXSound(Sound_Manager.TypeOfSound.sfx, Sound_Manager.SFX.soulNotification);
                UI_Manager._UI_MANAGER.UpdateUIPromptText(2);
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
                Debug.Log("StartTrigger");
                warnTrigger = false;
            }
        }
        
        if (other.gameObject.CompareTag("MissionTrigger"))
        {
            UI_Manager._UI_MANAGER.DeactivateUIPromptText();

            Mission_Manager._MISSION_MANAGER.StartMission();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("StartSoul"))
        {
            if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
            {
                UI_Manager._UI_MANAGER.StartTimer();
                Debug.Log("Pressed");
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
            }
        }
        if (other.gameObject.CompareTag("ObjectSoul"))
        {
            if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
            {
                UI_Manager._UI_MANAGER.StartTimer();
                Debug.Log("Pressed");
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
            }
        }
        if (other.gameObject.CompareTag("WarnSoul"))
        {
            if (Input_Manager._INPUT_MANAGER.GetActionChangeValue())
            {
                UI_Manager._UI_MANAGER.StartTimer();
                Debug.Log("Pressed");
                UI_Manager._UI_MANAGER.DeactivateUIPromptText();
                Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
            }
        }
    }
}
