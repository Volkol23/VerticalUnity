using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arachne_Behaviour : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform spot;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Mission_Manager._MISSION_MANAGER.GetCurrentMission().GetMissionObjective())
        {
            transform.position = spot.position;
        }
    }
}
