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

    [Header("MenuVariables")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject back;

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

    [Header("Fade")]
    [SerializeField] private GameObject backgroundFade;

    private void Awake()
    {
        if(_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(_UI_MANAGER);
        }
        else
        {
            _UI_MANAGER = this;
            DontDestroyOnLoad(gameObject);

            playButton.onClick.AddListener(GoToPlay);
            creditsButton.onClick.AddListener(GoToCredits);
            optionsButton.onClick.AddListener(GoToOptions);
            exitButton.onClick.AddListener(GoToExit);
            backButton.onClick.AddListener(GoBack);

            animatorFade = backgroundFade.GetComponent<Animator>();
        }
    }

    private void GoToPlay()
    {
        Game_Manager._GAME_MANAGER.GoToScene("Test_Level");
        FadeOut();
    }

    private void GoToOptions()
    {
        options.SetActive(true);
        back.SetActive(true);
        menu.SetActive(false);
        backButton.Select();
    }

    private void GoToCredits()
    {
        credits.SetActive(true);
        back.SetActive(true);
        menu.SetActive(false);
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
        menu.SetActive(true);
        playButton.Select();
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
