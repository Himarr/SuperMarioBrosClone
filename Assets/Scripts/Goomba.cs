using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float goombaVelocity;

    void Update()
    {
        //Movimiento del goomba
        gameObject.transform.Translate(goombaVelocity * Time.deltaTime, 0, 0);
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
}
