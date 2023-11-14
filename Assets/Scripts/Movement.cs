using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private bool isAccelerating;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }
    }
    private void FixedUpdate()
    {
        if (isAccelerating)
        {
            rb.velocity += transform.forward * acceleration * Time.deltaTime;
        }
    }
}
