using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeObject : MonoBehaviour
{
   
    public GameObject hasBook;
    public GameObject Dialog;
    public GameObject Text;
    private bool _isActive = false;
    private bool _isOnTrigger = false;
    private int _count = 0;
    private int rnd = 0;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _isOnTrigger = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        _isActive = false;
        _isOnTrigger = false;
        Dialog.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {

            if (Input.GetKey(KeyCode.E) && !_isActive && _isOnTrigger)
            {
                rnd = Random.Range(0, 3);
                Debug.Log(rnd);
                if (rnd >= 2)
                {
                    _count = 1;
                }

                if (hasBook != null && _count!= 1 && hasBook.CompareTag("Book"))
                {
                    if (rnd == 0)
                    {
                        Text.GetComponent<Text>().text = "Ты: О, конспект    |     Знания: +3";
                    }
                    if (rnd == 1)
                    {
                        Text.GetComponent<Text>().text = "Ты: Ура, я нашёл новую книгу! Или... старую...    |     Знания: +3";
                    }
                    Dialog.SetActive(true);
                }
                else
                {
                    Text.GetComponent<Text>().text = "Пусто...";
                    Dialog.SetActive(true);
                }
                
                _isActive = true;
                Debug.Log(_isActive);
            }
            else if (Input.GetKeyDown(KeyCode.E) && _isActive && _isOnTrigger)
            {
                Dialog.SetActive(false);
                _count = 1;
                _isActive = false;
            }
    }
}
