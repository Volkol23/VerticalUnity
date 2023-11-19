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

    [SerializeField] private Transform leftPointForce;
    [SerializeField] private Transform rightPointForce;
    [SerializeField] private Transform centerPointForce;

    [SerializeField] private Transform pointTransform;

    [SerializeField] private float steerSpeed;

    private float boatRotationY = 0f;
    private bool forward;

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
            forward = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = false;
        }


    }
    private void FixedUpdate()
    {
        Vector3 forceDirection = transform.forward;
        float steer = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            steer = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steer = -1f;
        }
        Vector3 forceToAdd = pointTransform.forward * currentSpeed;
        Vector3 forwardVector = Vector3.Scale(new Vector3(1f, 0f, 1f), transform.forward);

        //Rotation Force
        rb.AddForceAtPosition(steer * transform.right * steerSpeed, pointTransform.position);

        if (forward)
            ApplyForceToReachVelocity(rb, forwardVector * maxSpeed, acceleration);
        else if (!forward)
            ApplyForceToReachVelocity(rb, forwardVector * -maxSpeed, acceleration);
    }

    public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}
