using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngineInternal;

public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] static private float backwardsForcePenalty;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform steerPointTransform;
    [SerializeField] private Transform smoothPointTransform;
    [SerializeField] private Vector3 boatRotation;
    [SerializeField] private float rayDistanceCheck;
    [SerializeField] private float offsetForward;
    [SerializeField] private float offsetRight;
    [SerializeField] private float offsetLeft;

    [SerializeField] private float steerSpeed;

    [SerializeField] private Transform[] rayPositions;

    [SerializeField] private float collisionForce;

    [SerializeField] private Spline waterRiver;

    private float steerDirection;

    private Vector3 direction = Vector3.zero;

    private float currentTime;
    [SerializeField]
    private float maxChangeDirectionTime;
    [SerializeField]
    private bool collindingTimer;

    private Rigidbody rb;

    private Boat_Colliders colliders;

    private bool isInputPressed;

    //External objects
    private Camera mainCamera;

    Vector3 rayPositionForward;
    RaycastHit hitForward;
    RaycastHit hitRay;

    Vector3 normalCheck;

    [SerializeField]
    float m_Interpolation = .5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<Boat_Colliders>();
        backwardsForcePenalty = maxSpeed;
        mainCamera = Camera.main;
        direction = transform.forward;
    }

    private void Start()
    {
        //Spline spline = GetComponent<SplineContainer>()[0];
        //waterRiver.

        SplineUtility.Evaluate(waterRiver, m_Interpolation, out float3 position, out float3 direction, out float3 up);
        Debug.Log(direction);
    }
    private void Update()
    {
        if (Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            HandleInputs();
            rb.isKinematic = false;
        }
        else
        {
            currentSpeed = 0f;
            rb.isKinematic = true;
        }
        RaycastsBehaviour();
    }

    private void RaycastsBehaviour()
    {
        rayPositionForward = transform.position + transform.forward * offsetForward;

        if (Physics.Raycast(rayPositionForward, transform.forward, out hitForward, 50f))
        {
            if (hitForward.distance < rayDistanceCheck)
            {
                Debug.DrawRay(hitForward.point, hitForward.normal * rayDistanceCheck, Color.yellow);
                Vector3 directionForce = Vector3.Scale(new Vector3(1f, 0f, 1f), hitForward.normal);
                rb.AddForce(directionForce * collisionForce, ForceMode.Impulse);
                collindingTimer = true;
            }
            else
            {
                collindingTimer = false;
            }

            Debug.DrawRay(rayPositionForward, transform.forward * 1000, Color.white);
        }
        else
        {
            Debug.DrawRay(rayPositionForward, transform.forward * 1000, Color.red);
            collindingTimer = false;
        }

        foreach (Transform rayPos in rayPositions)
        {
            if(Physics.Raycast(rayPos.position, rayPos.forward, out hitRay, 50f))
            {
                if(hitRay.distance < rayDistanceCheck)
                {
                    Debug.DrawRay(hitRay.point, hitRay.normal * rayDistanceCheck, Color.yellow);
                    Vector3 directionForce = Vector3.Scale(new Vector3(1f, 0f, 1f), hitRay.normal);
                    rb.AddForce(directionForce * collisionForce, ForceMode.Impulse);
                }
                Debug.DrawRay(rayPos.position, rayPos.forward * 1000, Color.white);
            }
            else
            {
                Debug.DrawRay(rayPos.position, rayPos.forward * 1000, Color.red);
                collindingTimer = false;
            }
        }
    }

    private void HandleInputs()
    {
        steerDirection = Input_Manager._INPUT_MANAGER.GetSteerValue().x;
        if (Input_Manager._INPUT_MANAGER.GetAccelerateValue())
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            isInputPressed = true;
        }
        if (Input_Manager._INPUT_MANAGER.GetBrakeValue())
        {
            if (currentSpeed > -maxSpeed)
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }
            isInputPressed = true;
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
            isInputPressed = false;
        }
        if(currentSpeed > -1f)
        {
            steerDirection = -steerDirection;
        }
    }
    private void FixedUpdate()
    {
        RaycastsBehaviour();

        if (Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            Vector3 forwardVector = Vector3.Scale(new Vector3(1f, 0f, 1f), transform.forward);
            direction = forwardVector;

            Vector3 smoothVector = Vector3.Scale(new Vector3(1f, 0f, 1f), normalCheck);
            //Rotation Force
            if (currentSpeed != 0 && steerDirection != 0)
            {
                rb.AddForceAtPosition(steerDirection * transform.right * steerSpeed, steerPointTransform.position);

                rb.AddForceAtPosition(steerDirection * -transform.right * steerSpeed, smoothPointTransform.position);
            }

            //Water current
            
            //if (!colliders.GetColliding())
            //{
                ApplyForceToReachVelocity(rb, direction * maxSpeed, currentSpeed);
            //}
            //else if (collindingTimer && currentTime > 0f)
            //{
            //    Vector3 directionTest = (direction + smoothVector).normalized;
            //    ApplyForceToReachVelocity(rb, direction * maxSpeed, currentSpeed);
            //    rb.AddForceAtPosition(-transform.forward * maxSpeed / 8, smoothPointTransform.position);
            //}
        }
    }

    private void HandleRotationAutomatic()
    {
        if (currentSpeed != 0 && isInputPressed)
        {
            Quaternion rotation = Quaternion.LookRotation(new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z));
            rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rotation);
        }
    }

    public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

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

    public void SetDockPosition(Transform dockTransform)
    {
        rb.velocity = Vector3.zero;
        transform.position = dockTransform.position;
        transform.rotation = dockTransform.rotation;
    }

    public bool GetBoatStoped()
    {
        if(currentSpeed < 1f && currentSpeed > -1f && steerDirection == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.forward * 20);
        //Gizmos.DrawRay(hit.point, normal * 30);
        //Gizmos.DrawSphere(hit.point, 5f);
    }
}
