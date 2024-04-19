using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] public Player pl;
    float maximum = 100;
    float current;
    public Image mask;

    void Start()
    {
        current = 100;
    }

    void Update()
    {
        current = pl.fuel;
        getCurrentFill();
    }

    private void getCurrentFill()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }
}
