using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatTalks : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject Text;
    public GameObject Alert;
    public int BoxTaskComplete;
    private bool _isActive = false;
    public bool _firstTask = true;
    public bool MissedTask = false;

    private int n = 0;

    private int j = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive == true)
        {
            if (_firstTask == true )
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    n += 1;
                    Dialog.SetActive(true);
                }

                switch (n)
                {
                    case 1:
                        Text.GetComponent<Text>().text = "Кот: Говоришь с котом? Слышал что-нибудь о шизофрении?";
                        break;
                    case 2:
                        Text.GetComponent<Text>().text =
                            "Кот: Тебе бы не помешало расставить ящики.\n Зачем ты вообще притащил их в дом?";
                        Alert.SetActive(true);
                        break;

                    default:
                        Text.GetComponent<Text>().text = "Кот: Иди работай";

                        break;
                }
            }

            if (BoxTaskComplete == 3)
            {
                _firstTask = false;
                MissedTask = false;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    j += 1;
                    Dialog.SetActive(true);
                }

                switch (j)
                {
                    case 1:
                        Text.GetComponent<Text>().text = "Кот: Молодец, хоть что-то в твоей жизни становится лучше. ";
                        break;
                    case 2:
                        Text.GetComponent<Text>().text = "Кот: Интересно, что ты будешь делать дальше?";
                        break;
                    default:
                        Text.GetComponent<Text>().text = "Кот: Когда-нибудь узнаем...";

                        break;
                }
            }

            if (MissedTask == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    Text.GetComponent<Text>().text = "Кот: У всего есть последствия, запомни. ";
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
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        _isActive = false;
        Dialog.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
