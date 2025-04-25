using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public float koopaVelocity;
    public float shellVelocity;
    Animator anim;
    Rigidbody2D rb2D;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movimiento del koopa
        gameObject.transform.Translate(koopaVelocity * Time.deltaTime, 0, 0);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Rebote en la pared
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Breakable"))
        {
            koopaVelocity = koopaVelocity * -1;
        }

        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


    }

    public void koopaInShell()
    {
        gameObject.GetComponent<Animator>().SetBool("IsInShell", true);
        koopaVelocity = 0f;
        

    }

    public void ThrowShell()
    {
        if(anim.GetBool("IsInShell") == true)
        {
            gameObject.tag = "KoopaInShell";

        }
    }

    public void KoopaSlide()
    {
        Debug.Log("Mario patea al koopa");
        //rb2D.AddForce (new Vector2(shellVelocity, 0));
        //gameObject.transform.Translate(new Vector2(shellVelocity,0));

    }
}
