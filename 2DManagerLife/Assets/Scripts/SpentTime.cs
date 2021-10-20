using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpentTime : MonoBehaviour
{
    private bool _isActive;

    public GameObject Skip;
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;
    public GameObject Text;
    public GameObject Dialog;

    public GameObject tp1;
    public GameObject tp2;
    public GameObject tp3;

    public GameObject hs;

    public GameObject Miss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isActive == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (Skip.GetComponent<CatTalks>().BoxTaskComplete != 3)
                {
                    Miss.GetComponent<CatTalks>()._firstTask = false;
                    Miss.GetComponent<CatTalks>().MissedTask = true;
                    Text.GetComponent<Text>().text = "Вы проспали 5 часов";
                    Dialog.SetActive(true);
                    hs.GetComponent<HeroStats>().time -= 50f; 
                    Box1.transform.position = tp1.transform.position;
                    Box2.transform.position = tp2.transform.position;
                    Box3.transform.position = tp3.transform.position;
                    Box1.GetComponent<Rigidbody2D>().mass = 30;
                    Box2.GetComponent<Rigidbody2D>().mass = 30;
                    Box3.GetComponent<Rigidbody2D>().mass = 30;
                }
                else
                {
                    Text.GetComponent<Text>().text = "К сожалению вы опоздали на сон, спальня закрыта";
                    Dialog.SetActive(true);
                }
            }
            
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = true;
        }
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Dialog.SetActive(false);
        _isActive = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        
    }
}
