using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy_Object : MonoBehaviour
{
    [SerializeField] private Transform[] bouyancyPoints;

    [SerializeField] private float underWaterDrag = 3f;        //UnderWater Forces 
    [SerializeField] private float underWaterAngularDrag = 1f;
    [SerializeField] private float surfaceDrag = 0f;           //Surface Forces
    [SerializeField] private float surfaceAngularDrag = 0.05f;
    [SerializeField] private float floatingForce = 15f;        //Overall floating force
    [SerializeField] private float waterDepth = 0f;  //Water Depth
    [SerializeField] private float minWaterDepth;
    [SerializeField] private float maxWaterDepth;
    [SerializeField] private float waterLerp;

    [SerializeField]
    private float timeLerp;

    [SerializeField]
    private bool lerpChange;

    private bool underwater;
    private int bouyancyObjectsUnderWater;

    //Components of the gameObject
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timeLerp = 0f;
    }

    private void Update()
    {
        WaterLerpChange();
    }

    private void WaterLerpChange()
    {
        if (timeLerp > 0.99f)
        {
            lerpChange = false;
        }
        if (timeLerp < 0.01f)
        {
            lerpChange = true;
        }

        if (lerpChange)
        {
            timeLerp += waterLerp * Time.deltaTime;
        }
        else
        {
            timeLerp -= waterLerp * Time.deltaTime;
        }
        timeLerp = Mathf.Clamp01(timeLerp);
    }

    void FixedUpdate()
    {
        waterDepth = Mathf.Lerp(minWaterDepth, maxWaterDepth, timeLerp);
        //Reset bouyancy objects under water
        bouyancyObjectsUnderWater = 0;

        for (int i = 0; i < bouyancyPoints.Length; i++)
        {
            float difference = bouyancyPoints[i].position.y - waterDepth;

            if (difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingForce * Mathf.Abs(difference), bouyancyPoints[i].position, ForceMode.Force);

                bouyancyObjectsUnderWater += 1;

                if (!underwater)
                {
                    underwater = true;
                    SwitchUnderwaterState(true);
                }
            }
        }

        if (underwater && bouyancyObjectsUnderWater == 0)
        {
            underwater = false;
            SwitchUnderwaterState(false);
        }
    }

    void SwitchUnderwaterState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rb.drag = surfaceDrag;
            rb.angularDrag = surfaceAngularDrag;
        }
    }
}
