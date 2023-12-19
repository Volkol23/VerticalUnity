using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MissionObject", order = 1)]
public class Mission_Object : ScriptableObject
{
    [SerializeField] private string objectName;
    [SerializeField] private string objectPickUpText;
    [SerializeField] private int idObject;

    public string GetObjectName ()
    {
        return objectName;
    }
    public string GetObjectPickUpText()
    {
        return objectPickUpText;
    }
    public int GetId()
    {
        return idObject;
    }
}
