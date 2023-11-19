using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float riverSpeed;

    [SerializeField] private float acceleration;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] static private float backwardsForcePenalty = 20;

    [SerializeField] private Transform leftPointForce;
    [SerializeField] private Transform rightPointForce;
    [SerializeField] private Transform centerPointForce;

    [SerializeField] private Transform pointTransform;

    [SerializeField] private float steerSpeed;

    private float boatRotationY = 0f;
    private bool forward;
    private bool accelerating;
    private float steerDirection;

    private Quaternion OriginalRotation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        OriginalRotation = pointTransform.localRotation;
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
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (currentSpeed > -maxSpeed)
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (currentSpeed < 0f)
            {
                currentSpeed += acceleration * Time.deltaTime;
            } 
            else if(currentSpeed > 0f)
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }
        }

    }
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A) && currentSpeed != 0)
        {
            steerDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) && currentSpeed != 0)
        {
            steerDirection = -1f;
        }
        else
        {
            steerDirection = 0f;
        }
        Vector3 forwardVector = Vector3.Scale(new Vector3(1f, 0f, 1f), transform.forward);

        //Rotation Force
        rb.AddForceAtPosition(steerDirection * transform.right * steerSpeed, pointTransform.position);

        //Forward Force
        ApplyForceToReachVelocity(rb, forwardVector * maxSpeed, currentSpeed);
    }

    public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        //force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);
        if(force < 0f)
        {
            force /= backwardsForcePenalty; 
        }

        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            Vector3 velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}
