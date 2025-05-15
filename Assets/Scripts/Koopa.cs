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

    bool canMove = true;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Determina la direccion a la que se lanza el caparazon
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

        //Koopa se mete en el caparazon cuando Mario le salta encima
        if (player != null && player.jumpForce < 0 && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Koopa caparazon");

            koopaInShell();

            player.jumpForce += 10;
            player.bounceOnEnenemy = true;

            ThrowShell();
        }

        //Muerte del koopa
        if (collision.gameObject.CompareTag("KoopaInShell"))
        {
            KoopaDead();
        }

        //Koopa mata a Mario
        if (collision.gameObject.CompareTag("Player") && (koopaVelocity == 6 || koopaVelocity == -6))
        {
            Debug.Log("Mario muere :(");
            player.onHit();
        }

        //Caparazon reobta en las paredes
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Breakable"))
        {
            koopaVelocity = koopaVelocity * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Rebote en la pared
        if (collision.gameObject.CompareTag("Enemy"))
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

    public void KoopaDead()
    {
        rb2D.AddForce(new Vector2(12 * Time.deltaTime, 4 * Time.deltaTime));
        gameObject.GetComponent<Animator>().enabled = false;
        canMove = false;
        gameObject.layer = LayerMask.NameToLayer("NoColission");
        gameObject.GetComponent<SpriteRenderer>().flipY = true;

        Destroy(gameObject, 1f);
    }

}
