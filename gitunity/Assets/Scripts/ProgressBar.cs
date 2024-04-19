using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;

    void Start()
    {
        
    }

    void Update()
    {
        getCurrentFill();
    }

    private void getCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
