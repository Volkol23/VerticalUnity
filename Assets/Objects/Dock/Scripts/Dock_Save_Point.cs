using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock_Save_Point : MonoBehaviour
{
    [SerializeField]
    private bool savePoint = false;

    [SerializeField]
    private Transform dockPosition;
    [SerializeField]
    private Transform boatPosition;

    public void SetSavePoint()
    {
        savePoint = true;
    }

    public void EraseSavePoint()
    {
        savePoint = false;
    }

    public bool GetSavePoint()
    {
        return savePoint;
    }

    public Transform GetDockPositon()
    {
        return dockPosition;
    }

    public Transform GetBoatPosition()
    {
        return boatPosition;
    }
}
