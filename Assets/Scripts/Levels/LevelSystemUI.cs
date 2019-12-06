using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystemUI : MonoBehaviour
{

    private Slider expSlider;
    // Start is called before the first frame update
    void Start()
    {
        expSlider = GameObject.Find("EXPSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
