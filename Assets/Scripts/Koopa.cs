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

    bool inShell = false;

    public bool canMove = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Mario").GetComponent<Player>();

        boxCollider.offset = new Vector2(0.06f, 0);
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
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Breakable") || collision.gameObject.CompareTag("Enemy"))
        {
            if (inShell && collision.gameObject.CompareTag("Enemy"))
            {
                return;
            }
            koopaVelocity = koopaVelocity * -1;
        }

        if(anim.GetBool("IsInShell") == false)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
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

            KoopaInShell();

            player.AddVelocityY(15f);
            player.bounceOnEnenemy = true;

            ThrowShell();
        }

        if(collision.gameObject.CompareTag("FireBall") || collision.gameObject.CompareTag("Cappy"))
        {
            koopaDead();
        }

        if (player != null && collision.gameObject.CompareTag("Player") && anim.GetBool("IsInShell") == true && player.bounceOnEnenemy == false && player.GetVelocityY() == 0)
        {
            player.onHit();
        }

        if ((koopaVelocity == 6 || koopaVelocity == -6) && collision.gameObject.GetComponent<Koopa>())
        {
            collision.gameObject.GetComponent<Koopa>().koopaDead();
        }
    }

    public void KoopaInShell()
    {
        gameObject.GetComponent<Animator>().SetBool("IsInShell", true);
        inShell = true;

        koopaVelocity = 0f;

        gameObject.GetComponentInChildren<BoxCollider2D>().size = new Vector2(1.1f, 0);
    }

    public void ThrowShell()
    {
        if(anim.GetBool("IsInShell") == true)
        {
            gameObject.tag = "KoopaInShell";
        }
    }

    //Muerte del koopa
    public void koopaDead()
    {
        rb2D.AddForce(new Vector2(12 * Time.deltaTime, 4 * Time.deltaTime));
        gameObject.GetComponent<Animator>().enabled = false;
        canMove = false;
        gameObject.layer = LayerMask.NameToLayer("NoColission");
        gameObject.GetComponent<SpriteRenderer>().flipY = true;

        Destroy(gameObject, 1f);

        //puntos 
        GameManager.Instance.AddScore(100);
    }
}
