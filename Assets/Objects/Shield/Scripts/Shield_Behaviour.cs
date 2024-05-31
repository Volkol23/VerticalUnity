using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Shield_Behaviour : MonoBehaviour
{
    [SerializeField] private float direction;
    [SerializeField] private float speedMovement;
    [SerializeField] private bool isForward;

    [SerializeField] private float speedForward;
    [SerializeField] private float speedBackward;
    [SerializeField] private Transform finalPositionObject;

    private Vector3 position;
    [SerializeField]
    private Vector3 initPosition;
    [SerializeField]
    private Vector3 finalPosition;

    // Start is called before the first frame update
    void Awake()
    {
        position = transform.position;
        initPosition = transform.position;
        finalPosition = finalPositionObject.position;
        direction = 1f;
        isForward = true;
        speedMovement = speedForward;
    }

    // Update is called once per frame
    void Update()
    {
        MoveShields();
        SetUpSpeed();
    }

    private void MoveShields()
    {
        position += speedMovement * direction * transform.forward * Time.deltaTime;
        transform.position = position;
    }

    private void SetUpSpeed()
    {
        if (transform.position.z > finalPosition.z)
        {
            isForward = false;
        }
        if (transform.position.z < initPosition.z)
        {
            isForward = true;
        }

        if (isForward)
        {
            direction = 1f;
            speedMovement = speedForward;
        }
        else
        {
            direction = -1f;
            speedMovement = speedBackward;
        }
    }
}
