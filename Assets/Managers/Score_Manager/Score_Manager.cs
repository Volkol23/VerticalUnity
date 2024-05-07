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

    [SerializeField] private float percentageHealth;
    [SerializeField] private float percentageTime;

    [SerializeField] private GameObject[] starsUI;

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
        healthValue = health;
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
        percentageTime = (timeScore / maxPoints) * 100f;
    }

    public void CheckScore()
    {
        Debug.Log("Vida: " + healthValue);
        percentageHealth = (healthValue / maxPoints) * 100f;
        totalScore = healthValue + timeScore;
        if (objectPlus)
        {
            totalScore += maxPoints;
        }
        Debug.Log("Score Calculada" + totalScore);
    }

    public float UpdateTotalScore()
    {
        for(int i = 0; i < starsUI.Length; i++)
        {
            switch (i)
            {
                case 0:
                    starsUI[i].GetComponent<Star_Score_Behaviour>().StartFillScore(GetPercetageTime());
                    break;
                case 1:
                    starsUI[i].GetComponent<Star_Score_Behaviour>().StartFillScore(0f, GetItemValue());
                    break;
                case 2:
                    starsUI[i].GetComponent<Star_Score_Behaviour>().StartFillScore(GetPercentageHealth());
                    break;
            }
        }
        return totalScore;
    }

    public float GetPercentageHealth()
    {
        return percentageHealth;
    }

    public float GetPercetageTime()
    {
        return percentageTime;
    }

    public bool GetItemValue()
    {
        return objectPlus;
    }
}
