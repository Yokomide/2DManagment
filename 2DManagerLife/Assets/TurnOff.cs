using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOff : MonoBehaviour
{
    public void DisableBeginDialog()
    {
       gameObject.SetActive(false);
    }
}
