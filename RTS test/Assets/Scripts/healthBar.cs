using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxProgress(float max)
    {
        slider.maxValue = max;
        slider.value = 0;
    }
    public void SetProgress(float health)
    {
        slider.value = health;
    }
}
