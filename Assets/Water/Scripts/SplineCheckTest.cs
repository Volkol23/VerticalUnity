using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplineCheckTest : MonoBehaviour
{
    public SplineContainer splineContainer;
    public GameObject targetObject;

    void Start()
    {
        if (splineContainer == null || targetObject == null)
        {
            Debug.LogError("SplineContainer o targetObject not assigned.");
            return;
        }

        Spline spline = splineContainer[0];

        Vector3 targetPosition = targetObject.transform.position;
        float interpolation = 0.3f;
        float3 positionOnSpline;
        float3 directionOnSpline;
        float3 upOnSpline;
        //if (SplineUtility.Project(spline, targetPosition, out interpolation))
        //{
            SplineUtility.Evaluate(spline, interpolation, out positionOnSpline, out directionOnSpline, out upOnSpline);
            Debug.Log("Position on spline: " + positionOnSpline);
        //}
        //else
        //{
            Debug.LogError("Gameobject not on spline");
        //}
    }
}
