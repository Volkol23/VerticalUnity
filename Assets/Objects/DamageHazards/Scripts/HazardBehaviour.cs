using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{   
    private GameObject hazard;
    [SerializeField] private float distance;
    [SerializeField] private float damageTick;

    // Start is called before the first frame update
    void Start()
    {
        hazard = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            Debug.Log("HitDamage");
        }
    }

    public float GetDamageTick()
    {
        return damageTick;
    }
}
