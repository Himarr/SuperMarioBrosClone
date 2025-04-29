using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float goombaVelocity;
    Rigidbody2D rb2D;

    public bool canMove = true;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Movimiento del goomba
        if (canMove)
        {
            gameObject.transform.Translate(goombaVelocity * Time.deltaTime, 0, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Koopa koopa = gameObject.GetComponent<Koopa>();
        if (collision.gameObject.CompareTag("KoopaInShell"))
        {
            goombaDeadByShellOrFire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Rebote en la pared
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Breakable"))
    {
            goombaVelocity = goombaVelocity * -1;
        }
    }

    //Muerte del goomba
    public void goombaDead()
    {

        gameObject.layer = LayerMask.NameToLayer("OnlyGround");
        gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        goombaVelocity = 0f;
        Destroy(gameObject, 0.5f);

    }

    public void goombaDeadByShellOrFire()
    {
        rb2D.AddForce(new Vector2(12 * Time.deltaTime, 4 * Time.deltaTime));
        gameObject.GetComponent<Animator>().enabled = false;
        canMove = false;
        gameObject.layer = LayerMask.NameToLayer("NoColission");
        gameObject.GetComponent<SpriteRenderer>().flipY = true;
        
        Destroy(gameObject, 1f);
    }

    
}
