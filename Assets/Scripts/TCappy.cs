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

    public AudioClip boing;
    public AudioSource audioSource;

    public float speed;
    

    void Start()
    {
        initialPositionX = transform.position.x;

        player = GameObject.Find("Mario").GetComponent<Player>();

        
    }

    void Update()
    {
        positionX = transform.position.x;

        cappyDistance = initialPositionX - positionX;

        while (cappyDistance > -4f && cappyDistance < 4)
        {
            if (player.direction == 1)
            {
                gameObject.transform.Translate(speed * player.direction * Time.deltaTime, 0, 0);

            }else if (player.direction == -1)
            {
                gameObject.transform.Translate(speed * player.direction * Time.deltaTime, 0, 0);
            }


            break;
        }

        transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TestCappy testCappy = collision.gameObject.GetComponent<TestCappy>();
        // Mario rebota en Cappy o la recupera si no le salta encima
        if (collision.gameObject.CompareTag("Player") && player.GetVelocityY() < 0)
        {
            player.AddVelocityY(34f);
            audioSource.PlayOneShot(boing);
            Debug.Log("Mario toca a Cappy");
        }else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameObject.GetComponent<Animator>().SetTrigger("HaveCappy");
            
        }

        // Cappy mata a los goomba
        Goomba goomba = collision.gameObject.GetComponent<Goomba>();
        if (collision.gameObject.CompareTag("Enemy") && goomba != null)
        {
            goomba.goombaDeadByShellOrFire();

        }

        // Cappy mata a las planta pirania
        Piranhaplant piranhaplant = collision.gameObject.GetComponent<Piranhaplant>();
        if(collision.gameObject.CompareTag("MortalEnemy"))
        {
            piranhaplant.PiranhaDead();
        }

        // Cappy rompe bloques
        BreakableBlock breakableblock = collision.gameObject.GetComponent<BreakableBlock>();
        if (collision.gameObject.CompareTag("Breakable"))
        {
            Debug.Log("Cappy toca un bloque destructible");
            breakableblock.BlockBreak();
        }



    }
}
