using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    Player player;
    BoxCollider2D col;
    public float speed;

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
        yield return MoveToCenter(4f);
    }

    IEnumerator MoveToCenter(float time)
    {
        // ESTO ES UNA PUTISIMA MIERDA :)
        float pipeCenter = col.bounds.center.x;
        float elapsed = 0;

        while (elapsed < time)
        {
            player.transform.position = Vector2.Lerp(player.transform.position, new Vector2(pipeCenter, player.transform.position.y), elapsed / time);
            elapsed += Time.deltaTime;

            Debug.Log("Moviendo al centro");
            yield return null;
            
        }
        player.transform.position = new Vector3(pipeCenter, player.transform.position.y);

        Debug.Log("Terminado");
        player.canMove = true;
        yield return null;
    }
}
