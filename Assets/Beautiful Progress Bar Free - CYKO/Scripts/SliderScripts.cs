using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderScripts : MonoBehaviour
{
    public static SliderScripts singleton { get; private set; }

    private void Awake()
    {
        if(!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Slider slider;
    public Image fill;
    public bool _isFillSlider = false;
    public float newValue;
     void Update()
    {
        if (slider.value != newValue)
        {
            slider.value = Mathf.Lerp(slider.value, newValue, Time.deltaTime * 2f);
            fill.fillAmount = slider.value/100;
        }
    }

    public void SmoothBar(float energyPoints)
    {
        newValue = energyPoints;
    }
}
