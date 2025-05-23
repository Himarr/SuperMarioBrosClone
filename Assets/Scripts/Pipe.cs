using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    Player player;
    BoxCollider2D col;
    public float speed;
    public AudioClip pipeDown;
    public AudioSource audioSource;

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.S))
        { 
            // Bloquear movimiento
            player.canMove = false;

            StartCoroutine(PipeAnimation());
        }
        // Mover a mario al centro de la tuberia
        // Bajar a mario (3s)
        // Cambiar de escena
    }

    IEnumerator PipeAnimation()
    {
        Vector3 inicio = player.transform.position;
        Vector3 fin = new Vector3(col.bounds.center.x, inicio.y, inicio.z);
        yield return MoveObject(inicio, fin, 0.5f, player.gameObject);
        player.canMove=true;

        AudioSource.PlayClipAtPoint(pipeDown, gameObject.transform.position);
        inicio = player.transform.position;
        fin = new Vector3(inicio.x, inicio.y - 2);
        yield return MoveObject(inicio, fin, 1f, player.gameObject);
    }

    IEnumerator MoveObject(Vector3 inicio, Vector3 fin, float tiempo, GameObject gameObj)
    {
        float elapsed = 0;
        while (elapsed < tiempo)
        {
           
            gameObj.transform.position = Vector3.Lerp(inicio, fin, elapsed / tiempo);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObj.transform.position = fin;
    }
}
