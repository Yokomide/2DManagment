using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHint : MonoBehaviour
{
    [SerializeField] private GameObject E_hint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        E_hint.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        E_hint.SetActive(false);
    }

    private void Start()
    {
        E_hint.SetActive(false);
    }
}
