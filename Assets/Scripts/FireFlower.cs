using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : PowerUp
{
    void Start()
    {
        Player = GameObject.Find("Mario").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cambia el estado de mario y ejecuta la animación.
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player.currentStatus == "small" || Player.currentStatus == "big")
            {
                Player.Grow("Fire");
                Debug.Log("colision");
            }
        Destroy(gameObject);
        }
    }
}
