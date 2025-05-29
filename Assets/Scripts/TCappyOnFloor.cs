using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCappyOnFloor : MonoBehaviour
{

    //TestCappy testCappy;


    private void Start()
    {
        //testCappy = GameObject.Find("Mario").GetComponent<TestCappy>();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //testCappy.GetComponent<TestCappy>()
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Destroy(gameObject);
        }
    }
}
