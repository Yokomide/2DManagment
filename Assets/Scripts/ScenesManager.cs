using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private Color _colorA;
    private Color _colorB;

    [SerializeField] private Image Img;
    [Range(1, 10)] public float WaitingTime;
    [Range(1, 3)] public float BlackoutingTime;

    public Action WinAction;
    public Action LoseAction;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _colorB = Color.black;
        _colorA = new Color(_colorB.r, _colorB.g, _colorB.b, 0);
    }

    public void TryLoadScene(int index)
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (activeSceneIndex == index)
            return;

        if (activeSceneIndex == 0)
        {
            HomeDataSaver.Instance.SaveHomeData();
            VIDE_Data.VD.EndDialogue();
        }

        StartCoroutine(Blackouting(() =>
        {
            SceneManager.LoadScene(index);
            transform.position = new Vector3(0, 1000, 0);
        }));
    }

    private IEnumerator Blackouting(Action funct)
    {
        float currentBlackoutingTime = 0;

        while (currentBlackoutingTime < BlackoutingTime)
        {
            currentBlackoutingTime += Time.deltaTime;
            Img.color = Color.Lerp(_colorA, _colorB, currentBlackoutingTime / BlackoutingTime);
            yield return null;
        }

        funct();

        float curTime = 0;
        while (curTime < WaitingTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }
        while (currentBlackoutingTime > 0)
        {
            currentBlackoutingTime -= Time.deltaTime;
            Img.color = Color.Lerp(_colorA, _colorB, currentBlackoutingTime / BlackoutingTime);
            yield return null;
        }
    }

    public void ClearActions()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoseAction = null;
            WinAction = null;
        }
    }
}
