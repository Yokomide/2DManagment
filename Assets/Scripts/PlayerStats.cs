using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public float Energy;
    public float maxEnergy;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        maxEnergy = 100f;
        Energy = 80f;
        ChangeSlider();
    }

    public void AddEnergy(float energyPoints)
    {
        Energy += energyPoints;
        if (Energy >= maxEnergy)
        {
            Energy = maxEnergy;
        }
        ChangeSlider();
    }

    public void RemoveEnergy(float energyPoints)
    {
        Energy -= energyPoints;
        if (Energy <= 0)
        {
            Energy = 0;
        }
        ChangeSlider();
    }

    void ChangeSlider()
    {
        SliderScripts.Instance.SmoothBar(Energy);
    }
}
