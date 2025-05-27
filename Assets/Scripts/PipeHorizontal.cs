using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PipeHorizontal : MonoBehaviour
{
    Player player;
    BoxCollider2D col;
    public float speed;
    public AudioClip pipeDown;
    public AudioSource audioSource;
    bool trigger = false;
    float tiempo = 1 ;

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.D) && trigger == false &&(player.direction == 1))
        {
            StartCoroutine(AnimacionPipeR());
            AudioSource.PlayClipAtPoint(pipeDown, gameObject.transform.position);
        }

        if (Input.GetKey(KeyCode.A) && trigger == false && (player.direction == -1))
        {
            StartCoroutine(AnimacionPipeL());
            AudioSource.PlayClipAtPoint(pipeDown, gameObject.transform.position);
        }

        IEnumerator AnimacionPipeR()
        {
            Debug.Log("comienza corrutina");
            player.canMove = false;  
            Vector3 inicio = player.transform.position;
            Vector3 fin = new Vector3(inicio.x + 2f, inicio.y, inicio.z);
            float elapsed = 0;
            while (elapsed < tiempo)
            {
                player.transform.position = Vector3.Lerp(inicio, fin, elapsed / (tiempo));
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }

        IEnumerator AnimacionPipeL()
        {
            Debug.Log("comienza corrutina");
            player.canMove = false;
            Vector3 inicio = player.transform.position;
            Vector3 fin = new Vector3(inicio.x + -2f, inicio.y, inicio.z);
            float elapsed = 0;
            while (elapsed < tiempo)
            {
                player.transform.position = Vector3.Lerp(inicio, fin, elapsed / (tiempo));
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }
    }
}
