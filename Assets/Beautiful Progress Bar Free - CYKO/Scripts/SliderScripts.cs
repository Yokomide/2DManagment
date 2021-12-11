using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    public static SliderScripts Instance { get; private set; }

    public Slider slider;
    public Image fill;
    public bool _isFillSlider = false;
    public float newValue;
    
    
    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
