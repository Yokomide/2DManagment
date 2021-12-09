using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveChangeScene : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "Player") && (Input.GetKeyDown(KeyCode.E)))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        Application.LoadLevel("StoveGame");
    }
}
