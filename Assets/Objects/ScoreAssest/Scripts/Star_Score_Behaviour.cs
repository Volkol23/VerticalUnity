using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class Star_Score_Behaviour : MonoBehaviour
{
    [SerializeField] private float currentFillAmount;
    [SerializeField] private GameObject fillImage;
    [SerializeField] private float fillAmount;
    [SerializeField] private bool fillScore;
    [SerializeField] private float maxFillAmount;

    private Vector3 imagePosition;

    private void Awake()
    {
        imagePosition = fillImage.GetComponent<RectTransform>().anchoredPosition;
        maxFillAmount = fillImage.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (fillScore)
        {
            if (currentFillAmount < fillAmount)
            {
                imagePosition.y += currentFillAmount;
                currentFillAmount += Time.deltaTime;
                fillImage.GetComponent<RectTransform>().anchoredPosition = imagePosition;
                if (imagePosition.y > 0)
                {
                    fillScore = false;
                }
            }
        }
    }

    public void StartFillScore(float scoreToFill, bool isObject = false)
    {
        if (isObject)
        {
            fillAmount = maxFillAmount;
        } 
        else
        {
            fillAmount = scoreToFill;
        }
        fillScore = true;
    }
}
