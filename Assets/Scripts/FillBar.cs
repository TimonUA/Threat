﻿using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{

    // Unity UI References
    public Slider slider;
    public Text displayText;

    // Create a property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            if (slider.value > 0 && slider.value < 100)
            {
                displayText.text = (slider.value).ToString("0.0") + "%";
            }
            else 
            {
                displayText.text = (slider.value).ToString("0") + "%";
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        CurrentValue = 0f;
    }
}