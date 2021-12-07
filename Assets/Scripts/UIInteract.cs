using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class UIInteract : MonoBehaviour
{
    public GameObject phone;

    private Animator _anim;
    private void Start()
    {
        _anim = phone.transform.GetChild(1).GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenPhone();
        }
    }

    public void OpenPhone()
    {
        if (phone.activeInHierarchy)
        {
            _anim.Play("RemovePhone");
                phone.SetActive(false);
        }
        else
        {
            phone.SetActive(true);
            _anim.Play("TakePhone");
        }
    }

    public void CallChecker()
    {

    }
   
}
