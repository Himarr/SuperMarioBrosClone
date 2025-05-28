using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBlock : MonoBehaviour
{
    Player player;
    float time = 0.25f;

    bool isEmpty = false;
    public bool isInvisible = false;

    public PowerUp powerUp;
    SpriteRenderer spriteRenderer;
    public AudioClip bump;
    public AudioClip moneda;
    public AudioSource audioSource;

    public GameObject coin;
    public PowerUp fireFlower;
    public bool hasCoin = false;

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (isInvisible)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Compara la posici�n m�s baja del bloque y la m�s alta de Mario.

        float contactY = collision.GetContact(0).point.y;
        float maxPosition = player.col.bounds.max.y;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + 0.5f);

        if (collision.gameObject.CompareTag("Player") && contactY > maxPosition)
        {
            gameObject.GetComponent<Animator>().SetBool("QuestionBlockOff", true);

            if (!isEmpty)
            {
                AudioSource.PlayClipAtPoint(bump, gameObject.transform.position);
                StartCoroutine(MoveCoroutine(startPosition, endPosition, time, this.gameObject));
                isEmpty = true;
                spriteRenderer.enabled = true;

                if (hasCoin)
                {
                    StartCoroutine(SpawnCoin(startPosition, endPosition + new Vector3(0, 1.5f), 0.2f));
                    hasCoin = false;
                    isEmpty = true;
                    return;
                }
            }
        }
    }
    IEnumerator MoveCoroutine(Vector3 startPosition, Vector3 endPosition, float time, GameObject gameObject)
    {
        
        // Subir
        yield return StartCoroutine(MoveObject(startPosition, endPosition, time, gameObject));
        // Bajar
        yield return StartCoroutine(MoveObject(endPosition, startPosition, time, gameObject));
        // Spawnear Power Up
        yield return StartCoroutine(SpawnPowerUp(startPosition, new Vector3(transform.position.x, transform.position.y + 1), time = 0.5f));

    }
    IEnumerator MoveObject(Vector3 startPosition, Vector3 endPosition, float time, GameObject gameObject)
    {
        // Mueve un objeto desde un punto A a otro punto B en un tiempo determinado.
        float elapsed = 0;

        while (elapsed < time)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endPosition;
    }

    //El bloque solo suelta champi��n (mario peque�o) y flor de fuego (mario grande)
    IEnumerator SpawnPowerUp(Vector3 startPosition, Vector3 endPosition, float time)
    {
        if (powerUp != null)
        {
            if (powerUp.GetType() == typeof(Mushroom) && player.currentStatus != "small")
            {
                powerUp = fireFlower;
            }
            GameObject powerUpObject = Instantiate(powerUp, startPosition, Quaternion.identity).gameObject;
            yield return MoveObject(startPosition, endPosition, time, powerUpObject);
            
            if (powerUp.GetType() == typeof(Mushroom))
            {
                powerUp.canMove = true;
            }
        }
    }
    IEnumerator SpawnCoin(Vector3 startPosition, Vector3 endPosition, float time)
    {
        if (coin != null)
        {
            GameObject coinObject = Instantiate(coin, startPosition, Quaternion.identity).gameObject;
            yield return MoveObject(startPosition, endPosition, time, coinObject);
            GameManager.Instance.AddCoins();
            GameManager.Instance.AddScore(100);
            AudioSource.PlayClipAtPoint(moneda, gameObject.transform.position);

            Destroy(coinObject);
        }
    }
}



