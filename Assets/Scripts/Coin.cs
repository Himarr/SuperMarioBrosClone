using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(coin, gameObject.transform.position);
            GameManager.Instance.AddCoins();
            Destroy(gameObject);

            //aniadir puntos 
            GameManager.Instance.AddScore(200); 


        }

       
    }
}
