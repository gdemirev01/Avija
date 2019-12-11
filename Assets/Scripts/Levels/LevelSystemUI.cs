using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystemUI : MonoBehaviour
{

    public Slider expSlider;

    public void SetExpBarLimits(float minValue, float maxValue)
    {
        expSlider.minValue = minValue;
        expSlider.maxValue = maxValue;
    }

    public void SetExpBarValue(float value)
    {
        expSlider.value = value;
    }
}
