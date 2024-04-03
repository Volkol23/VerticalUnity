using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Behaviour : MonoBehaviour
{
    [SerializeField] private Collider[] colliderVortex;
    [SerializeField] private float effectRadius = 0;
    [SerializeField] private float gravityForce = 9.8f;


    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private LayerMask layers;

    // Update is called once per frame
    void Update()
    {
        colliderVortex = Physics.OverlapSphere(transform.position, effectRadius, layers);

        foreach (Collider c in colliderVortex)
        {
            Vector3 attractionDir = transform.position - c.transform.position;

            c.attachedRigidbody.AddForce(attractionDir * gravityForce, ForceMode.Force);
        }
    }

    void OnDrawGizmos()
    {

        Gizmos.color = gizmoColor;

        Gizmos.DrawWireSphere(transform.position, effectRadius);

    }
}
