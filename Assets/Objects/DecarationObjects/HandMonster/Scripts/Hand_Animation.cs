using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Animation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}
