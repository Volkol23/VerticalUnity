using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Behaviour : MonoBehaviour
{
    [SerializeField]
    private Collider trigger;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mission_Manager._MISSION_MANAGER.GetCurrentMission().GetCompleteMission())
        {
            trigger.enabled = true;
        }
        else
        {
            trigger.enabled = false;
        }
    }
}
