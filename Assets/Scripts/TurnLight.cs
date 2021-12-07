using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLight : MonoBehaviour
{
    private bool playerInRange;
    private GameObject LightChild;
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }
    // Update is called once per frame
    void Start()
    {
        LightChild = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& playerInRange)
        {
            if(LightChild.activeInHierarchy)
            {
                LightChild.SetActive(false);
            }
            else
            {
                LightChild.SetActive(true);
            }
        }
    }
}
