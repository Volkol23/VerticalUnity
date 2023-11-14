using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float riverSpeed;

    [SerializeField] private float acceleration;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private Transform leftPointForce;
    [SerializeField] private Transform rightPointForce;
    [SerializeField] private Transform centerPointForce;

    [SerializeField] private Transform pointTransform;

    [SerializeField] private float rotationSpeed;

    private float boatRotationY = 0f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += 1f * acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (currentSpeed > 0f)
            {
                currentSpeed -= 1* acceleration * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0f;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            boatRotationY = pointTransform.localEulerAngles.y + rotationSpeed * Time.deltaTime;

            if (boatRotationY > 5f && boatRotationY < 270f)
            {
                boatRotationY = 5f;
            }

            Vector3 newRotation = new Vector3(0f, boatRotationY, 0f);

            pointTransform.localEulerAngles = newRotation;
        }
        //else
        //{
        //    pointTransform.localEulerAngles = Vector3.zero;
        //}

        if (Input.GetKey(KeyCode.D))
        {
            boatRotationY = pointTransform.localEulerAngles.y - rotationSpeed * Time.deltaTime;

            if (boatRotationY < 355f && boatRotationY > 90f)
            {
                boatRotationY = 355f;
            }

            Vector3 newRotation = new Vector3(0f, boatRotationY, 0f);

            pointTransform.localEulerAngles = newRotation;
        }
        //else
        //{
        //    pointTransform.localEulerAngles = Vector3.zero;
        //}
    } 
    private void FixedUpdate()
    {
        Vector3 forceToAdd = pointTransform.forward * currentSpeed;

        rb.AddForceAtPosition(forceToAdd, pointTransform.position);
        //if (isAccelerating)
        //{
        //    rb.AddForceAtPosition(transform.forward * forwardForcePower, centerPointForce.position, ForceMode.Force);
        //} 

        //if (isLeft && isAccelerating)
        //{
        //    rb.AddForceAtPosition(-transform.right * turnForcePower, leftPointForce.position, ForceMode.Force);
        //}

        //if (isRight && isAccelerating)
        //{
        //    rb.AddForceAtPosition(transform.right * turnForcePower, leftPointForce.position, ForceMode.Force);
        //}
        
    }
}
