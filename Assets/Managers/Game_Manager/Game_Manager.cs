using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager _GAME_MANAGER;

    [SerializeField]
    private GameGeneral currentGeneral;

    private MissionType missionLevel;

    private Transform playerPosition;
    private Transform boatPosition;

    private float health;
    [SerializeField]
    private float cooldownDamage;
    private float cooldownTime;

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
        cooldownTime = 0f;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 0 - MainMenu // 1 - Main Story // 2 - Intro Hades // 3 - Level 1 Arachne // 4 - Level 2 Minotaur // 5 - Level 3 Ice // 6 - Mechanic Scene
        switch (scene.buildIndex)
        {
            case 0:
                ChangeGeneral(GameGeneral.MENU);
                missionLevel = MissionType.NOMISSION;
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.mainMenu);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case 1:
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.mainStory);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case 2:
                ChangeGeneral(GameGeneral.MENU);
                missionLevel = MissionType.HADES;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.introScene);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case 3:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ARACNHE;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.level1);
                UI_Manager._UI_MANAGER.ActivateGameUI();
                UI_Manager._UI_MANAGER.UpdateCurrentRiver(3);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SetHealthDamage();
                SaveDock();
                break;
            case 4:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.MINOTAUR;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.level2);
                UI_Manager._UI_MANAGER.UpdateCurrentRiver(4);
                SaveDock();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SetHealthDamage();
                break;
            case 5:
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ICEWOLF;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.mainStory);
                UI_Manager._UI_MANAGER.UpdateCurrentRiver(5);
                SaveDock();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SetHealthDamage();
                break;
            case 6: // Test Mechanic Scene
                ChangeGeneral(GameGeneral.PLAYER);
                missionLevel = MissionType.ARACNHE;
                Mission_Manager._MISSION_MANAGER.StartMission();
                Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.level1);
                UI_Manager._UI_MANAGER.ActivateGameUI();
                UI_Manager._UI_MANAGER.UpdateCurrentRiver(3);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SetHealthDamage();
                SaveDock();
                break;
        }
    }

    private void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetResetValue())
        {
            ResetPlayerPosition();
        }
        cooldownTime += Time.deltaTime;
        if(health <= 0)
        {
            ResetPlayerPosition();
            health = 100;
        }
    }
    public void ChangeGeneral(GameGeneral gameGeneralState)
    {
        currentGeneral = gameGeneralState;
    }

    private void SetHealthDamage()
    {
        health = 100f;
    }

    public void GetDamage(float damage)
    {
        if(cooldownTime > cooldownDamage)
        {
            health -= damage;
            Debug.Log(health);
            cooldownTime = 0f;
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
        Debug.Log("sceneIndex" + sceneIndex);
        StartCoroutine(LoadSceneCorutine(sceneIndex));
    }

    IEnumerator LoadSceneCorutine(int  sceneIndex)
    {
        UI_Manager._UI_MANAGER.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        UI_Manager._UI_MANAGER.FadeIn();
    }
    
    public void ResetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Character_Behaviour>().SetDockPosition(playerPosition);

        GameObject boat = GameObject.FindGameObjectWithTag("Boat");
        boat.GetComponent<Movement>().SetDockPosition(boatPosition);
    }

    public void SaveDock()
    {
        GameObject[] docks = GameObject.FindGameObjectsWithTag("Dock");

        Dock_Save_Point savePoint;
        foreach (GameObject dock in docks)
        {
            savePoint = dock.GetComponent<Dock_Save_Point>();
            if (savePoint.GetSavePoint())
            {
                playerPosition = savePoint.GetDockPositon();
                boatPosition = savePoint.GetBoatPosition();
            }
        }
        playerPosition.rotation = Quaternion.identity;
        //boatPosition.rotation = Quaternion.identity;
    }
}
