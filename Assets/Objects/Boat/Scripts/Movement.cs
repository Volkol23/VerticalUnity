using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngineInternal;

public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] static private float backwardsForcePenalty;

    [SerializeField] private Transform steerPointTransform;
    [SerializeField] private Transform smoothPointTransform;
    [SerializeField] private float rayDistanceCheck;
    [SerializeField] private float offsetForward;
    [SerializeField] private float offsetRight;
    [SerializeField] private float offsetLeft;

    [SerializeField] private float steerSpeed;

    [SerializeField] private Transform[] rayPositions;

    private float steerDirection;

    private float currentTime;
    [SerializeField]
    private float maxChangeDirectionTime;
    [SerializeField]
    private bool collindingTimer;

    private Rigidbody rb;

    private Boat_Colliders colliders;

    Vector3 rayPositionForward;
    Vector3 rayPositionRight;
    Vector3 rayPositionLeft;
    RaycastHit hitForward;
    RaycastHit hitRight;
    RaycastHit hitLeft;
    RaycastHit hitPos;

    Vector3 normalCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<Boat_Colliders>();
        backwardsForcePenalty = maxSpeed;
    }
    private void Update()
    {
        rayPositionForward = transform.position + transform.forward * offsetForward;
        rayPositionRight = transform.position + transform.right * offsetRight;
        rayPositionLeft = transform.position + transform.right * offsetLeft;
        if (Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            HandleInputs();
        }
        if(Physics.Raycast(rayPositionForward, transform.forward, out hitForward, 50f))
        {
            if(hitForward.distance < rayDistanceCheck)
            {
                Debug.DrawRay(hitForward.point, hitForward.normal, Color.yellow);
                normalCheck = hitForward.normal;
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
        if (Physics.Raycast(rayPositionRight, transform.right, out hitRight, 50f))
        {
            Debug.DrawRay(rayPositionRight, transform.right * 1000, Color.white);
            if (hitRight.distance < rayDistanceCheck)
            {
                Debug.DrawRay(hitRight.point, hitRight.normal, Color.yellow);
                normalCheck = hitRight.normal;
                collindingTimer = true;
            }
            else
            {
                collindingTimer = false;
            }
        }
        else
        {
            Debug.DrawRay(rayPositionRight, transform.right * 1000 * 1000, Color.red);
            collindingTimer = false;
        }
        if (Physics.Raycast(rayPositionLeft, -transform.right, out hitLeft, 50f))
        {
            Debug.DrawRay(rayPositionLeft, -transform.right * 1000, Color.white);
            if (hitLeft.distance < rayDistanceCheck)
            {
                Debug.DrawRay(hitLeft.point, hitLeft.normal, Color.yellow);
                normalCheck = hitLeft.normal;
                collindingTimer = true;
            }
            else
            {
                collindingTimer = false;
            }
        }
        else
        {
            Debug.DrawRay(rayPositionLeft, -transform.right * 1000 * 1000, Color.red);
            collindingTimer = false;
        }

        if (collindingTimer && currentTime < maxChangeDirectionTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
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

            Vector3 smoothVector = Vector3.Scale(new Vector3(1f, 0f, 1f), normalCheck);
            //Rotation Force
            if (currentSpeed != 0)
            {
                rb.AddForceAtPosition(steerDirection * transform.right * steerSpeed, steerPointTransform.position);

                rb.AddForceAtPosition(steerDirection * -transform.right * steerSpeed, smoothPointTransform.position);
            }

            if (!colliders.GetColliding())
            {
                ApplyForceToReachVelocity(rb, forwardVector * maxSpeed, currentSpeed);
            }
            else if (collindingTimer && currentTime > 0f)
            {
                Vector3 direction = (forwardVector + smoothVector).normalized;
                ApplyForceToReachVelocity(rb, direction * maxSpeed, currentSpeed);
            }
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

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.forward * 20);
        //Gizmos.DrawRay(hit.point, normal * 30);
        //Gizmos.DrawSphere(hit.point, 5f);
    }
}
