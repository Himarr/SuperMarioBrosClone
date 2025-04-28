using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBlock : MonoBehaviour
{
    Player player;
    public bool isMoving;
    public float time;

    void Start()
    {
        isMoving = false;
        player = GameObject.Find("Mario").GetComponent<Player>();
    }


    void Update()
    {


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float contactY = collision.GetContact(0).point.y;
        float maxPosition = player.col.bounds.max.y;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + 0.5f);

        if (collision.gameObject.CompareTag("Player") && contactY > maxPosition && player);

        {
            gameObject.GetComponent<Animator>().SetBool("QuestionBlockOff", true);

            StartCoroutine(Mover(startPosition, endPosition, time));

            Debug.Log("Bloque ? ");

        }
    }
    IEnumerator Mover(Vector3 startPosition, Vector3 endPosition, float time)
    {
        // Subir
        yield return StartCoroutine(MoveObject(startPosition, endPosition, time));
        // Bajar
        yield return StartCoroutine(MoveObject(endPosition, startPosition, time));

    }
    IEnumerator MoveObject(Vector3 inicio, Vector3 fin, float tiempo)
    {
        float elapsed = 0;
        Debug.Log("inicio");
        while (elapsed < tiempo)
        {
            transform.position = Vector3.Lerp(inicio, fin, elapsed / tiempo);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = fin;
        Debug.Log("fin");   
    }

    //El bloque solo suelta champiñón (mario pequeño) y flor de fuego (mario grande)

}



