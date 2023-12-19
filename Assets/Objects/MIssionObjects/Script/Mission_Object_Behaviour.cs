using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Object_Behaviour : MonoBehaviour
{
    [SerializeField] private Mission_Object itemMission;
    
    public int GetObjectId()
    {
        return itemMission.GetId();
    }

    public void ObjectGet()
    {
        gameObject.SetActive(false);
        Mission_Manager._MISSION_MANAGER.GetMissionObject(GetObjectId());
    }
}
