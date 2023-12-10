using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hades_Behaviour : MonoBehaviour
{
    private Animator animator;

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
        //animator.SetTrigger("Talk");
        //animator.SetTrigger("Stop");
    }

     
}
