using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager _UI_MANAGER;

    [SerializeField] private static Canvas canvas;

    [Header("MenuVariables")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject menu;

    [Header("PauseVariables")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button backMenuButton;

    [Header("GameUI")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject abilitys;

    [SerializeField] private GameObject missionObjective;
    [SerializeField] private TMP_Text missionOnbjectiveText;

    [SerializeField] private GameObject currentRiver;

    private Animator animatorFade;
    private GameGeneral gameGeneralPause;

    [Header("Fade")]
    [SerializeField] private GameObject backgroundFade;

    private bool pauseMenuActive = false;

    private void Awake()
    {
        if (_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(_UI_MANAGER);
        }
        else
        {
            _UI_MANAGER = this;
            DontDestroyOnLoad(_UI_MANAGER);

            playButton.onClick.AddListener(GoToPlay);
            creditsButton.onClick.AddListener(GoToCredits);
            optionsButton.onClick.AddListener(GoToOptions);
            exitButton.onClick.AddListener(GoToExit);
            backButton.onClick.AddListener(GoBack);

            backMenuButton.onClick.AddListener(GoToMainMenu);
            resumeButton.onClick.AddListener(Resume);

            animatorFade = backgroundFade.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetPauseValue())
        {
            Pause();
        }
    }
    private void GoToPlay()
    {
        FadeOut();
        Game_Manager._GAME_MANAGER.GoToScene("Test_Level");
        menu.SetActive(false);
    }

    private void GoToOptions()
    {
        options.SetActive(true);
        back.SetActive(true);
        menuButtons.SetActive(false);
        backButton.Select();
    }

    private void GoToCredits()
    {
        credits.SetActive(true);
        back.SetActive(true);
        menuButtons.SetActive(false);
        backButton.Select();
    }

    private void GoToExit()
    {
        Application.Quit();
    }

    private void GoBack()
    {
        options.SetActive(false);
        credits.SetActive(false);
        back.SetActive(false);
        menuButtons.SetActive(true);
        playButton.Select();
    }

    private void GoToMainMenu()
    {
        Game_Manager._GAME_MANAGER.GoToScene("MainMenu");
        menu.SetActive(true);
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
        Debug.Log("MainMenu");
    }

    private void Resume()
    {
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
        Game_Manager._GAME_MANAGER.ChangeGeneral(gameGeneralPause);
        Debug.Log("Resume");
        Time.timeScale = 1;
    }

    private void Pause()
    {
        if (pauseMenu)
        {
            pauseMenu.SetActive(true);
        }
        Time.timeScale = 0;
        gameGeneralPause = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
        Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
        resumeButton.Select();
    }
    public void FadeIn()
    {
        animatorFade.Play("FadeIn");
    }

    public void FadeOut()
    {
        animatorFade.Play("FadeOut");
    }

    public void ActivatePause()
    {
        pauseMenu.SetActive(true);
    }

    public void ActivateAbilities()
    {
        abilitys.SetActive(true);
    }

    public void ActivateMissionObjective(string objectiveText)
    {
        missionObjective.SetActive(true);
        missionOnbjectiveText.text = objectiveText;
    }

    public void ActivateCurrentRiver()
    {
        currentRiver.SetActive(true);
    }

    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void DeactivateAbilities()
    {
        abilitys.SetActive(false);
    }

    public void DeactivateMissionObjective()
    {
        missionObjective.SetActive(false);
    }

    public void DeactivateCurrentRiver()
    {
        currentRiver.SetActive(false);
    }
}