using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Behaviour : MonoBehaviour
{
    enum CameraMode
    {
        MANUAL,
        AUTOMATIC
    };

    [Header("Camera Variables")]
    [SerializeField] private GameObject target;
    [SerializeField] private float minTargetDistance;
    [SerializeField] private float maxTargetDistance;
    [SerializeField] private float targetDistance;
    [SerializeField] private float cameraLerp;
    [SerializeField] private float automaticCameraLerp;
    [SerializeField] private float sensivity;

    [SerializeField] private float maxRotation;
    [SerializeField] private float minRotation;
    [SerializeField] private float rotationXautomatic;

    [SerializeField] private CameraMode cameraMode;

    [SerializeField] private float rotationSpeedAutomatic;

    private float changeModeTime = 2f;
    private float currentTime;


    private GameGeneral gameGeneral;

    private float rotationX;
    private float rotationY;

    private RaycastHit hitInfo;

    private float timeAutomatic;
    private bool automatic;

    private GameObject dialogueCameraView;

    int layerMask;
    private void Awake()
    {
        SetTarget("CameraTarget");
        layerMask = 7;
        cameraMode = CameraMode.AUTOMATIC;
        currentTime = 0f;
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
        if (currentTime > changeModeTime)
        {
            cameraMode = CameraMode.AUTOMATIC;
            currentTime = 0f;
        }
        if (Input_Manager._INPUT_MANAGER.GetCameraRotationValue().y != 0 || Input_Manager._INPUT_MANAGER.GetCameraRotationValue().x != 0)
        {
            rotationX = transform.eulerAngles.x;
            rotationY = transform.eulerAngles.y;
            cameraMode = CameraMode.MANUAL;
            currentTime = 0f;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    private void LateUpdate()
    {
        switch (cameraMode)
        {
            case CameraMode.MANUAL:
                ManualMode();
                break;
            case CameraMode.AUTOMATIC:
                AutomaticMode();
                break;
        }

        if (Mission_Manager._MISSION_MANAGER.GetInDialogue() || UI_Manager._UI_MANAGER.GetPauseActive())
        {

        }
        else
        {
            if(gameGeneral == GameGeneral.PLAYER)
            {
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
            else if (gameGeneral == GameGeneral.BOAT)
            {
                //Apply smooth movement to the Camera
                Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);

                //Check if there are objects in between
                if (Physics.Linecast(target.transform.position, finalPosition, out hitInfo, layerMask))
                {
                    finalPosition = hitInfo.point;
                }
                transform.position = finalPosition;           
            }
        }
    }

    private void ManualMode()
    {
        //Handle Inputs 
        rotationX += Input_Manager._INPUT_MANAGER.GetCameraRotationValue().y * sensivity;
        rotationY += Input_Manager._INPUT_MANAGER.GetCameraRotationValue().x * sensivity;

        //Control min and max angle of the camera
        rotationX = Mathf.Clamp(rotationX, minRotation, maxRotation);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }

    private void AutomaticMode()
    {
        Quaternion finalRotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, automaticCameraLerp * Time.deltaTime);
        transform.eulerAngles = new Vector3(rotationXautomatic, finalRotation.eulerAngles.y, 0f);
    }

    private void SetupBoat()
    {
        targetDistance = 20f;
        cameraLerp = 5f;
        sensivity = 0.1f;
        minRotation = 0;
        maxRotation = 60f;

        SetTarget("BoatCamera");
    }

    private void SetupPlayer()
    {
        targetDistance = 25f;
        cameraLerp = 12f;
        sensivity = 0.2f;
        minRotation = -40;
        maxRotation = 50f;

        SetTarget("CameraTarget");

    }
    private void SetTarget(string tag)
    {
       target = GameObject.FindGameObjectWithTag(tag);
    }
}
