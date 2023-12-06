using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] static private float backwardsForcePenalty;

    [SerializeField] private Transform pointTransform;

    [SerializeField] private float steerSpeed;

    private float steerDirection;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        backwardsForcePenalty = maxSpeed;
    }
    private void Update()
    {
        if(Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            HandleInputs();
        }
    }

    private void HandleInputs()
    {
        if (Input_Manager._INPUT_MANAGER.GetAccelerateValue())
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        if (Input_Manager._INPUT_MANAGER.GetBrakeValue())
        {
            if (currentSpeed > -maxSpeed)
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }
        }
        if (!Input_Manager._INPUT_MANAGER.GetAccelerateValue() && !Input_Manager._INPUT_MANAGER.GetBrakeValue())
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
        steerDirection = Input_Manager._INPUT_MANAGER.GetSteerValue().x;
    }
    private void FixedUpdate()
    {
        if (Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            Vector3 forwardVector = Vector3.Scale(new Vector3(1f, 0f, 1f), transform.forward);

            //Rotation Force
            if (currentSpeed != 0)
            {
                rb.AddForceAtPosition(steerDirection * transform.right * steerSpeed, pointTransform.position);
            }

            //Forward Force
            ApplyForceToReachVelocity(rb, forwardVector * maxSpeed, currentSpeed);
        }
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

    public void SetDockPosition()
    {
        rb.velocity = Vector3.zero;
    }
}
