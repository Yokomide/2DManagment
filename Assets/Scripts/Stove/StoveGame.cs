using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveGame : MonoBehaviour
{
    private bool soap = false;
    private bool water = false;
    private bool sponge = false;

    private int circles = 0;
    private int tryCount = 0;

    public GameObject lose;
    public GameObject win;

    public GameObject false1; //наши крестики
    public GameObject false2;
    public GameObject false3;

    public GameObject dirt1;//все виды загрязнения и вода с пеной
    public GameObject dirt2;
    public GameObject dirt3;
    public GameObject waterSprite;
    public GameObject soapSprite;

    void Start()
    {
        
    }

    void Update()    //при ошибке игрока ставим по крестику, всего их 3
    {
        if(tryCount == 1)
        {
            false1.SetActive(true);
        }


        if(tryCount == 2)
        {
            false2.SetActive(true);
        }

        if(tryCount == 3)
        {
            false3.SetActive(true);
            Lose();
        }
    }

    public void Soap()
    {
        if(water == false && sponge == false) //Если ни вода ни губка не юзались, то все ок
        {
            soap = true;
            soapSprite.SetActive(true);

        }
        else  //В ином случае ошибка
        {
            tryCount += 1;
        }
        
    }

    public void Water() //Если юзалась пена, но не юзалась губка, то все ок
    {
        if(soap == true && sponge == false)
        {
            water = true;
            waterSprite.SetActive(true);
        }
        else //В ином случае ошибка
        {
            tryCount += 1;
        }
    }

    public void Sponge() //Если и вода и губка юзались, то все ок
    {
        if(water == true && soap == true)
        {
            soap = false;
            water = false;
            soapSprite.SetActive(false);
            waterSprite.SetActive(false);
            dirt3.SetActive(false);

            if(circles == 1) //Несколько циклов удаления грязи
            {
                dirt2.SetActive(false);
            }

            if(circles == 2)
            {
                dirt1.SetActive(false);
                Win();
            }

            circles += 1;
        }
        else //В ином случае ошибка
        {
            tryCount += 1;
        }

    }

    void Lose() //Включает меню проигрыша 
    {
        lose.SetActive(true);
        Time.timeScale = 0f;
    }

    void Win() //Включает меню выигрыша
    {
        win.SetActive(true);
        Time.timeScale = 0f;
    }
    

}
