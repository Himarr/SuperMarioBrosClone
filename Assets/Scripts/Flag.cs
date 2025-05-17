using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Player player;
    BoxCollider2D col;
    Animation anim;

    public float moveSpeed;
    public float time;



    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
          //transform.position = collision.transform.position;
        }
    }


    void Update()
    {
        
    }
}
