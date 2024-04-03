using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Position : MonoBehaviour
{
    [SerializeField] private float minYPosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float lerpPosition;

    bool lerpChange;
    float timeLerp;

    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        LerpChanger();
        position.x = transform.position.x;
        position.y = Mathf.Lerp(minYPosition, maxYPosition, timeLerp);
        position.z = transform.position.z;

        transform.position = position;
    }

    private void LerpChanger()
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
            timeLerp += lerpPosition * Time.deltaTime;
        }
        else
        {
            timeLerp -= lerpPosition * Time.deltaTime;
        }
        timeLerp = Mathf.Clamp01(timeLerp);
    }
}
