using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Animation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}
