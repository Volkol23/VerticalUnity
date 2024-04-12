using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Behaviour : MonoBehaviour
{
    [SerializeField] private float soulMode;
    [SerializeField] private Transform soulPosition;

    void Start()
    {
        soulPosition = GameObject.FindGameObjectWithTag("SoulPosition").transform;
    }

    
    void Update()
    {
        transform.position = soulPosition.transform.position;
    }
}
