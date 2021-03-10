using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

    public void Start()
    {
		slider.maxValue = 1f;
		slider.value = 0f;
	}
    public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

	public void SetCurrentHealth(float health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
