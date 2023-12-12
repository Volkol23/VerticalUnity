using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager _GAME_MANAGER;

    [SerializeField]
    private GameGeneral currentGeneral;

    private MissionType missionLevel;

    [SerializeField]
    private GameObject lastDock;

    private void Awake()
    {
        if(_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _GAME_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }

        currentGeneral = GameGeneral.MENU;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 0 - MainMenu // 1 - Main Story // 2 - Intro Hades // 3 - Level 1 Arachne // 4 - Level 2 Minotaur // 5 - Level 3 Ice 
        switch (scene.buildIndex)
        {
            case 0:
                ChangeGeneral(GameGeneral.MENU);
                missionLevel = MissionType.NOMISSION;
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.mainMenu);
                break;
            case 1:
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.mainStory);
                break;
            case 2:
                ChangeGeneral(GameGeneral.MENU);
                missionLevel = MissionType.HADES;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.introScene);
                break;
            case 3:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ARACNHE;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.level1);
                UI_Manager._UI_MANAGER.ActivateGameUI();
                break;
            case 4:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.MINOTAUR;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.level2);
                break;
            case 5:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ICEWOLF;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.mainStory);
                break;
        }
    }

    private void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetResetValue())
        {
            if (currentGeneral == GameGeneral.BOAT)
            {
                GameObject boat = GameObject.FindGameObjectWithTag("Boat");
            }

        }

    }
    public void ChangeGeneral(GameGeneral gameGeneralState)
    {
        currentGeneral = gameGeneralState;
        if(currentGeneral == GameGeneral.PLAYER)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //player.transform.position = player.GetComponent<InteractiveObjects>().GetClosestDock().position;
        }
        if(currentGeneral == GameGeneral.BOAT)
        {
            GameObject boat = GameObject.FindGameObjectWithTag("Boat");
            boat.GetComponent<Movement>().SetDockPosition();
        }
    }

    public GameGeneral GetCurrentGeneral()
    {
        return currentGeneral;
    }

    public MissionType GetMissionLevel()
    {
        return missionLevel;
    }
    
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCorutine(sceneIndex));
    }

    IEnumerator LoadSceneCorutine(int  sceneIndex)
    {
        UI_Manager._UI_MANAGER.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        UI_Manager._UI_MANAGER.FadeIn();
    }
    
    public void ResetBoat()
    {

    }
}
