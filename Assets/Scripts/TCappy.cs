using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TCappy : MonoBehaviour
{

    public float initialPositionX;
    public float positionX;
    public float cappyDistance;

    public Player player;

    

    void Start()
    {
        initialPositionX = transform.position.x;

        player = GameObject.Find("MarioWithCappy").GetComponent<Player>();

        
    }

    void Update()
    {
        positionX = transform.position.x;

        cappyDistance = initialPositionX - positionX;

        while ((cappyDistance > -4f))
        {
            gameObject.transform.Translate(0.03f, 0, 0);

            break;
        }

        transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TestCappy testCappy = collision.gameObject.GetComponent<TestCappy>();
        //Mario rebota en Cappy o la recupera si no le salta encima
        if (collision.gameObject.CompareTag("Player") && player.jumpForce < 0)
        {
            player.jumpForce += 34;

            Debug.Log("Mario toca a Cappy");
        }else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameObject.GetComponent<Animator>().SetTrigger("HaveCappy");
            //testCappy.nextShot -= 2;
            
        }

        //Cappy mata a los goomba
        Goomba goomba = collision.gameObject.GetComponent<Goomba>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            goomba.goombaDeadByShellOrFire();

        }

        //Cappy rompe bloques
        BreakableBlock breakableblock = collision.gameObject.GetComponent<BreakableBlock>();
        if (collision.gameObject.CompareTag("Breakable"))
        {
            Debug.Log("Cappy toca un bloque destructible");
            breakableblock.BlockBreak();
        }



    }
}
