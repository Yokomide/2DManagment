using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrder : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        gameObject.transform.position = player.GetComponent<Transform>().position;
    }
}
