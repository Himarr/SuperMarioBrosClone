using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKeyDown(KeyCode.D) && trigger == false)
        {
            Vector3 inicio = player.transform.position;
            Vector3 fin = new Vector3(inicio.x += 2f, inicio.y, inicio.z);


        }
    }
}
