using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    public static Menu_Manager _MENU_MANAGER;

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

    private void Awake()
    {
        if(_MENU_MANAGER != null && _MENU_MANAGER != this)
        {
            Destroy(_MENU_MANAGER);
        }
        else
        {
            _MENU_MANAGER = this;
            DontDestroyOnLoad(gameObject);

            playButton.onClick.AddListener(GoToPlay);
            creditsButton.onClick.AddListener(GoToCredits);
            optionsButton.onClick.AddListener(GoToOptions);
            exitButton.onClick.AddListener(GoToExit);
            backButton.onClick.AddListener(GoBack);
        }
    }

    private void GoToPlay()
    {

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
}
