using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplineCheckTest : MonoBehaviour
{
    public SplineContainer splineContainer;
    public GameObject targetObject;
    private Spline spline;

    void Start()
    {
        if (splineContainer == null || targetObject == null)
        {
            Debug.LogError("SplineContainer o targetObject not assigned.");
            return;
        }

        spline = splineContainer[0];

        float toutnerarest;
        float3 nearestPosition;
        SplineUtility.GetNearestPoint<Spline>(spline, transform.position, out nearestPosition, out toutnerarest);
        Vector3 targetPosition = targetObject.transform.position;
        float interpolation = 3f;
        //float3 position = SplineUtility.Evaluate<Spline>(spline, interpolation);
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
            Debug.Log("Gameobject not on spline");
        //}
    }

    private void Update()
    {
        float toutnerarest;
        float3 nearestPosition;
        SplineUtility.GetNearestPoint<Spline>(spline, transform.position, out nearestPosition, out toutnerarest);
        Vector3 targetPosition = targetObject.transform.position;
        float interpolation = 3f;

        float3 positionOnSpline;
        float3 directionOnSpline;
        float3 upOnSpline;

        SplineUtility.Evaluate(spline, interpolation, out positionOnSpline, out directionOnSpline, out upOnSpline);
        Debug.Log("Position on spline: " + positionOnSpline);
    }
}
