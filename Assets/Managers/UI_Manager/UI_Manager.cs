using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager _UI_MANAGER;


    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject backgroundFade;
    [SerializeField] private GameObject abilitys;
    [SerializeField] private GameObject missionObjective;
    [SerializeField] private GameObject currentRiver;

    [SerializeField]
    private Color fadeInColor;
    [SerializeField]
    private Color fadeOutColor;

    private Animator fade;
    private void Awake()
    {
        if(_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(_UI_MANAGER);
        }
        else
        {
            _UI_MANAGER = this;
            DontDestroyOnLoad(_UI_MANAGER);

            fade = backgroundFade.GetComponent<Animator>();
        }
    }

    public void ActivatePause()
    {
        pauseMenu.SetActive(true);
    }

    public void ActivateAbilities()
    {
        abilitys.SetActive(true);
    }

    public void ActivateMissionObjective()
    {
        missionObjective.SetActive(true);
    }

    public void ActivateCurrentRiver ()
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

    public void FadeIn()
    {
        fade.Play("FadeOut");
    }

    public void FadeOut()
    {
        fade.Play("FadeOut");
    }
}
