using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject ct;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shelf"))
        {
            rb = other.GetComponent<Rigidbody2D>();
   
            ct.GetComponent<CatTalks>().BoxTaskComplete += 1;
        }
    }
}
