using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MonsterRun : MonoBehaviour
{

    [SerializeField] SplineContainer spline;
    [SerializeField] float speed;
    [SerializeField] bool running; 

    [SerializeField] Spline splines;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartCorsue()
    {

    }
}
