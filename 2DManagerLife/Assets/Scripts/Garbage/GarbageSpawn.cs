using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawn : MonoBehaviour
{

    public GameObject garbage;

    private int sec = 0;

    void Start()
    {

    }

    
    void Update()
    {
        if(sec > 10)
        {
            garbage.SetActive(true);
        }
    }

    IEnumerator spawn()
    {
        while(true)
        {
            sec++;
            yield return new WaitForSeconds(1);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "Player") && (Input.GetKeyDown(KeyCode.E)))
        {
            garbage.SetActive(false);
            sec = 0;
            StartCoroutine(spawn());
        }
    }
}
