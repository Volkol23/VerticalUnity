using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancyObject : MonoBehaviour
{
    [SerializeField] private Transform[] bouyancyPoints;

    [SerializeField] private float underWaterDrag = 3f;        //UnderWater Forces 
    [SerializeField] private float underWaterAngularDrag = 1f;
    [SerializeField] private float surfaceDrag = 0f;           //Surface Forces
    [SerializeField] private float surfaceAngularDrag = 0.05f;
    [SerializeField] private float floatingForce = 15f;        //Overall floating force
    [SerializeField] private float waterDepth = 0f;            //Water Depth

    private bool underwater;
    private int bouyancyObjectsUnderWater;

    //Components of the gamneObject
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
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
