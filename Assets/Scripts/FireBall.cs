using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    int dir;

    Player player;
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        dir = player.dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime * dir, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Posición más baja de la bola de fuego
        float lowPos = boxCollider.bounds.min.y;
        // Posición más alta de la colisión
        float contactPos = collision.GetContact(0).point.y;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Matar enemigo y destruir bola
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Breakable"))
        {
            if (lowPos > contactPos)
            {
                Debug.Log("Rebote");
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
