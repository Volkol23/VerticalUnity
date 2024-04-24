using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Score_Manager _SCORE_MANAGER;

    [SerializeField] private float totalScore;
    [SerializeField] private float maxPoints;
    [SerializeField] private bool objectPlus;
    [SerializeField] private float healthValue;
    [SerializeField] private float timeScore;

    [SerializeField] private float defaultFinishTime;

    private void Awake()
    {
        if (_SCORE_MANAGER != null && _SCORE_MANAGER != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _SCORE_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GetHealthValue(float health)
    {
        healthValue = health * 0.5f;
    }

    public void ActivateObjectPlus()
    {
        objectPlus = true;
    }

    public void CalculateTimeScore(float totalTimeEnlapsed)
    {
        Debug.Log("Tiempo Total: " + totalTimeEnlapsed);
        float contableTime = totalTimeEnlapsed - defaultFinishTime;
        Debug.Log("Timepo Para Score: " + contableTime);
        if (contableTime < 0f)
        {
            timeScore = maxPoints;
        }
        else if (contableTime < 5f)
        {
            timeScore = maxPoints;
        }
        else
        {
            timeScore = maxPoints - contableTime;
        }
        Debug.Log("TimeScore" + timeScore);
    }

    public void CheckScore()
    {
        totalScore = healthValue + timeScore;
        if (objectPlus)
        {
            totalScore += maxPoints;
        }
        Debug.Log("Score Calculada" + totalScore);
    }

    public float UpdateTotalScore()
    {
       return totalScore;
    }
}
