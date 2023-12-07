using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Mission", order = 1)]
public class Mission : ScriptableObject
{
    [SerializeField] private string missionName;
    [SerializeField] private MissionType missionType;
    [SerializeField] private string missionObjectiveText;

    [SerializeField] private MissionObject[] objects;
    [SerializeField] private string missionObjectText;


    [SerializeField] private string missionStartText;
    [SerializeField] private string missionCompletedText;

    private bool objectiveCompleted = false;

    private bool missionCompleted = false;

    public string GetMissionObjectiveText() 
    { 
        return missionObjectiveText;
    }

    public string GetMissionObjectText(int id)
    {
        foreach (MissionObject item in objects)
        {
            if(item.GetId() == id)
            {
                missionObjectText = item.GetObjectPickUpText();
            }
        }
        return missionObjectText;
    }
    
    public MissionType GetMissionType()
    {
        return missionType;
    }
    public string GetMissionStartText()
    {
        return missionStartText;
    }

    public string GetMissionCompletedText ()
    {
        return missionCompletedText;
    }

    public int GetObjectsNumber()
    {
        return objects.Length;
    }
    public void CompleteMission()
    {
        missionCompleted = true;
    }

    public void CompleteObjective()
    {
        objectiveCompleted = true;
    }
}
