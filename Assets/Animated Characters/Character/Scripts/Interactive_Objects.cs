using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Objects : MonoBehaviour
{
    [SerializeField] private Transform seatTransform;

    private GameObject closestDock;
    private Transform dockTransform;
    private Vector3 forward;

    private void Awake()
    {
        forward = seatTransform.forward;
    }
    private void Update()
    {
        if(Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            transform.position = seatTransform.position;
            transform.forward = forward;
            transform.rotation = seatTransform.rotation;
        }
    }

    private void GetDockPosition()
    {
        GameObject[] docks = GameObject.FindGameObjectsWithTag("Dock");

        float proximity = 100000000f;
        foreach(GameObject dock in docks)
        {
            float buffer = Vector3.Distance(dock.transform.position, transform.position);
            dock.GetComponent<Dock_Save_Point>().EraseSavePoint();
            if (buffer < proximity)
            {
                proximity = buffer;
                closestDock = dock;
            }
        }
        dockTransform = closestDock.transform;
        closestDock.GetComponent<Dock_Save_Point>().SetSavePoint();
        Game_Manager._GAME_MANAGER.SaveDock();
    }

    public Transform GetClosestDock()
    {
        GetDockPosition();
        return dockTransform;
    }
}
