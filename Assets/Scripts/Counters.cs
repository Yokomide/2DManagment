using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counters : MonoBehaviour
{
    public static Counters Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int _flowerDryness = 40;
    public int FlowerDryness
    {
        get
        {
            return _flowerDryness;
        }

        set
        {
            _flowerDryness = Mathf.Clamp(_flowerDryness + value, 0, 100);
        }
    }


    private int _dirtyStove;
    public int DirtyStove
    {
        get
        {
            return _dirtyStove;
        }

        set
        {
            _dirtyStove = Mathf.Clamp(_dirtyStove + value, 0, 100);
        }
    }
}
