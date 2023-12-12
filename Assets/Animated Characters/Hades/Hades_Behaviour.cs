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
        Mission_Manager._MISSION_MANAGER.InitMission();
    }

    private void Update()
    {
        animator.SetBool("Talk", Mission_Manager._MISSION_MANAGER.GetInDialogue());
    }
}
