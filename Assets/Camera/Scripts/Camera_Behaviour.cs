using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Behaviour : MonoBehaviour
{
    [Header("Camera Variables")]
    [SerializeField] private GameObject target;
    [SerializeField] private float targetDistance;
    [SerializeField] private float cameraLerp;
    [SerializeField] private float sensivity;

    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;

    private GameGeneral gameGeneral;

    private float rotationX;
    private float rotationY;

    private RaycastHit hitInfo;

    private GameObject DialogueCameraView;

    int layerMask;
    private void Awake()
    {
        SetTarget();

        layerMask = 7;
    }

    private void Update()
    {
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
        switch (gameGeneral)
        {
            case GameGeneral.PLAYER:
                SetupPlayer();
                break;
            case GameGeneral.BOAT:
                SetupBoat();
                break;
        }
    }
    private void LateUpdate()
    {
        if (Mission_Manager._MISSION_MANAGER.GetInDialogue() || UI_Manager._UI_MANAGER.GetPauseActive())
        {

        }
        else
        {
            //Handle Inputs 
            rotationX += Input_Manager._INPUT_MANAGER.GetCameraRotationValue().y * sensivity;
            rotationY += Input_Manager._INPUT_MANAGER.GetCameraRotationValue().x * sensivity;

            //Control min and max angle of the camera
            rotationX = Mathf.Clamp(rotationX, minRotation, maxRotation);

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

            //Apply smooth movement to the Camera
            Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);

            //Check if there are objects in between
            if (Physics.Linecast(target.transform.position, finalPosition, out hitInfo, layerMask))
            {
                //if(hitInfo.)
                finalPosition = hitInfo.point;
            }

            transform.position = finalPosition;
        }
        
    }

    private void SetupBoat()
    {
        targetDistance = 30f;
        cameraLerp = 3f;
        sensivity = 0.1f;
        minRotation = 0;
        maxRotation = 60f;

        SetTarget();
    }

    private void SetupPlayer()
    {
        targetDistance = 25f;
        cameraLerp = 12f;
        sensivity = 0.2f;
        minRotation = -40;
        maxRotation = 50f;

        SetTarget();

    }
    private void SetTarget()
    {
        if(gameGeneral == GameGeneral.PLAYER)
        {
            target = GameObject.FindGameObjectWithTag("CameraTarget");
        }
        if(gameGeneral == GameGeneral.BOAT)
        {
            target = GameObject.FindGameObjectWithTag("Boat");
        }
    }
}
