using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hades_Behaviour : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float maxTimerCooldown;
    [SerializeField] private string[] introDialogues;

    [SerializeField] private GameObject obstacleObject;

    private float currentTimer;
    [SerializeField]
    private int indexDialogue = 0;
    private bool setupDialogue = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UI_Manager._UI_MANAGER.ActivateDialogueBox();

    }
    private void Update()
    {
        if (setupDialogue)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer > maxTimerCooldown)
            {
                currentTimer = 0;
                UI_Manager._UI_MANAGER.FadeOut();
                //StartCoroutine(DialogueFadeOut());
                UI_Manager._UI_MANAGER.UpdateDialogueText(introDialogues[indexDialogue]);
                indexDialogue++;
                UI_Manager._UI_MANAGER.FadeIn();
                if (indexDialogue == introDialogues.Length)
                {
                    obstacleObject.SetActive(false);
                    setupDialogue = false;
                }
            }
        }
       
        //animator.SetTrigger("Talk");
        //animator.SetTrigger("Stop");
    }

    IEnumerator DialogueFadeOut()
    {
        yield return new WaitForSeconds(0.3f);
        UI_Manager._UI_MANAGER.UpdateDialogueText(introDialogues[indexDialogue]);
        indexDialogue++;
    } 
}
