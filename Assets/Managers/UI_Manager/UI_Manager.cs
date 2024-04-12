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
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject missionObjective;
    [SerializeField] private TMP_Text missionOnbjectiveText;
    [SerializeField] private TMP_Text currentRiver;
    [SerializeField] private string[] currentRiverText;
    [SerializeField] private GameObject uiPrompt;
    [SerializeField] private TMP_Text uiPromptText;
    [SerializeField] private string[] promptStringText;

    [Header("DialogueUI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private Button nextDialogueButton;

    private Animator animatorFade;
    private GameGeneral gameGeneralPause;

    [Header("Fade")]
    [SerializeField] private GameObject backgroundFade;

    private bool pauseActive;

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

            nextDialogueButton.onClick.AddListener(NextDialogue);

            animatorFade = backgroundFade.GetComponent<Animator>();
        }
    }
    private void GoToPlay()
    {
        Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.MAINSTORY);
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
        pauseMenu.SetActive(false);
        menu.SetActive(true);
        Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.MAINMENU);
    }

    private void Resume()
    {
        Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.BOAT);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pauseActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pauseActive = true;

        gameGeneralPause = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
        Game_Manager._GAME_MANAGER.ChangeGeneral(GameGeneral.MENU);
        resumeButton.Select();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool GetPauseActive()
    {
        return pauseActive;
    }
    private void NextDialogue()
    {
        Mission_Manager._MISSION_MANAGER.NextDialogue();
    }

    public void FadeIn()
    {
        animatorFade.SetTrigger("Start");
    }

    public void FadeOut()
    {
        animatorFade.SetTrigger("End");
    }

    public void ActivateGameUI()
    {
        gameUI.SetActive(true);
    }

    public void DeactivateGameUI()
    {
        gameUI.SetActive(false);
    }
    public void ActivatePause()
    {
        pauseMenu.SetActive(true);
    }

    public void ActivateMissionObjective(string objectiveText)
    {
        missionObjective.SetActive(true);
        missionOnbjectiveText.text = objectiveText;
    }

    public void UpdateCurrentRiver(int index)
    {
        switch (index)
        {
            case (int)SceneIndex.LEVEL1:
                currentRiver.text = currentRiverText[0];
                break;
            case (int)SceneIndex.LEVEL2:
                currentRiver.text = currentRiverText[1];
                break;
            case (int)SceneIndex.LEVEL3:
                currentRiver.text = currentRiverText[2];
                break;
        }
    }

    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ActivateDialogueBox()
    {
        dialogueBox.SetActive(true);
        nextDialogueButton.Select();
    }

    public void UpdateDialogueText(string dialogue)
    {
        dialogueText.text = dialogue;
    }
    public void DeactivateDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
    public void DeactivateMissionObjective()
    {
        missionObjective.SetActive(false);
    }

    public void ActivateUIPromptText()
    {
        uiPrompt.SetActive(true);
        Debug.Log("Activate");
    }

    public void DeactivateUIPromptText()
    {
        uiPrompt.SetActive(false);
    }

    // 0 - Coger objetos 1 - Continue action
    public void UpdateUIPromptText(int idText) 
    {
        uiPromptText.text = promptStringText[idText];
    }
}