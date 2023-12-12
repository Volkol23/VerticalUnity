using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Behaviour : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravity;

    //Movement Variables
    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 lastDirection = Vector3.zero;

    private float accelerationIncrease;

    //Components of the gameObject
    private CharacterController characterController;

    //External objects
    private Camera mainCamera;

    private void Awake()
    {
        //Initialize components and variables
        characterController = GetComponent<CharacterController>();
        direction = transform.forward;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.PLAYER)
        {
            HandleInputs();
            HandleRotation();
            HandleMovement();
            HandleGravity();
        }
        else
        {
            speed = 0;
        }
    }

    private void HandleInputs()
    {
        //Handle input values form the movement
        if (Input_Manager._INPUT_MANAGER.GetMovement().magnitude != 0)
        {
            direction = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f)
            * new Vector3(Input_Manager._INPUT_MANAGER.GetMovement().x, 0f, Input_Manager._INPUT_MANAGER.GetMovement().y);
            lastDirection = direction;
        }
        accelerationIncrease = Input_Manager._INPUT_MANAGER.GetMovement().magnitude;

        //Normaliza direction vectors
        direction.Normalize();
        lastDirection.Normalize();
    }

    private void HandleMovement()
    {
        //Movement behaviour with acceleration
        if (accelerationIncrease > 0f)
        {
            speed += acceleration * Time.deltaTime;
        }
        else if (accelerationIncrease <= 0f)
        {
            speed -= deacceleration * Time.deltaTime;
        }

        //Handle max and min speed
        speed = Mathf.Clamp(speed, 0f, maxSpeed);

        finalVelocity.x = lastDirection.x * speed;
        finalVelocity.z = lastDirection.z * speed;

        //Move character controller

        //Update character controller transform when it has a character controller
        Physics.SyncTransforms();
        characterController.Move(finalVelocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        //Roatate character relative to the forward of the camera
        float rotation = Vector3.SignedAngle(direction, -transform.forward, transform.up);

        if (direction != transform.forward /*&& rotation > rotationThreshold*/)
        {
            transform.Rotate(Vector3.up * rotation * Time.deltaTime * rotationSpeed);
        }
    }

    private void HandleGravity()
    {
        //Apply gravity
        lastDirection.y = -1f;

        //Gravity behaviour
        if (characterController.isGrounded)
        {
            finalVelocity.y = lastDirection.y * gravity * Time.deltaTime;
        }
        else
        {
            finalVelocity.y += lastDirection.y * gravity * Time.deltaTime;
        }

        //characterController.la
    }

    public void SetDockPosition(Transform dockTransform)
    {
        transform.position = dockTransform.position;
        transform.rotation = dockTransform.rotation;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
