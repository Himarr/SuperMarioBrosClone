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

    public PowerUp powerUp;

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Compara la posición más baja del bloque y la más alta de Mario.

        float contactY = collision.GetContact(0).point.y;
        float maxPosition = player.col.bounds.max.y;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + 0.5f);

        if (collision.gameObject.CompareTag("Player") && contactY > maxPosition)
        {
            gameObject.GetComponent<Animator>().SetBool("QuestionBlockOff", true);

            if (!isEmpty)
            {
                StartCoroutine(MoveCoroutine(startPosition, endPosition, time, this.gameObject));
                isEmpty = true;
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

    //El bloque solo suelta champiñón (mario pequeño) y flor de fuego (mario grande)
    IEnumerator SpawnPowerUp(Vector3 startPosition, Vector3 endPosition, float time)
    {
        if (powerUp != null)
        {
            
            GameObject powerUpObject = Instantiate(powerUp, startPosition, Quaternion.identity).gameObject;
            yield return MoveObject(startPosition, endPosition, time, powerUpObject);
            
            if (powerUp.GetType() == typeof(Mushroom))
            {
                powerUp.canMove = true;
            }
        }
    }
}



