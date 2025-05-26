using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public float koopaVelocity;
    public float shellVelocity;
    Animator anim;
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider;
    Player player;

    public bool canMove = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Mario").GetComponent<Player>();
    }

    void Update()
    {
        //Movimiento del koopa
        if (canMove)
        {
            gameObject.transform.Translate(koopaVelocity * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTrigger"))
        {
            canMove = true;
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float koopaCenter = boxCollider.bounds.center.x;
        float marioCenter = player.col.bounds.center.x;

        if(koopaCenter > marioCenter)
        {
            if (player != null && gameObject.tag == "KoopaInShell")
            {
                koopaVelocity = 6;
                rb2D.AddForce(new Vector2(koopaVelocity * Time.deltaTime, 0));
                Debug.Log("Koopa a la derecha de mario");
            }
        }
        else if (marioCenter > koopaCenter)
        {
            if (player != null && gameObject.tag == "KoopaInShell")
            {
                koopaVelocity = -6;
                rb2D.AddForce(new Vector2(koopaVelocity * Time.deltaTime, 0));
                Debug.Log("Koopa a la izquierda de mario");
            }
        }

        if (player != null && player.GetVelocityY() < 0 && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Koopa caparazon");

            koopaInShell();

            player.AddVelocityY(15f);
            player.bounceOnEnenemy = true;

            ThrowShell();
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
}
