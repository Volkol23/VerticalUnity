using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager _GAME_MANAGER;

    private GameGeneral currentGeneral;

    private MissionType missionLevel;

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
        // 0 - MainMenu // 1 - Level 1 Arachne // 2 - Level 2 Minotaur // 3 - Level 3 Ice // 4 - Intro Hades
        switch (scene.buildIndex)
        {
            case 0:
                ChangeGeneral(GameGeneral.MENU);
                missionLevel = MissionType.NOMISSION;
                break;
            case 1:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ARACNHE;
                break;
            case 2:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.MINOTAUR;
                break;
            case 3:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ICEWOLF;
                break;
            case 4:
                ChangeGeneral(GameGeneral.MENU);
                missionLevel = MissionType.HADES;
                break;
        }
    }

    public void ChangeGeneral(GameGeneral gameGeneralState)
    {
        currentGeneral = gameGeneralState;
        if(currentGeneral == GameGeneral.PLAYER)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = player.GetComponent<InteractiveObjects>().GetClosestDock().position;
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
    
    public void GoToScene(string sceneName)
    {
        StartCoroutine(LoadSceneCorutine(sceneName));
    }

    IEnumerator LoadSceneCorutine(string sceneName)
    {
        UI_Manager._UI_MANAGER.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        UI_Manager._UI_MANAGER.FadeIn();
    }
}
