using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Player player;
    BoxCollider2D col;
    Animator anim;

    public float moveSpeed;
    public float tiempo;
    public float coords = -3.5f;
    




    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
        anim = player.GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.canMove = false;
            anim.SetBool("isFlagDown", true); 
            StartCoroutine(MarioDown());

        }
    }

    IEnumerator MarioDown()
    {
        Vector3 inicio = player.transform.position;
        Vector3 fin = new Vector3(inicio.x, coords, inicio.z);
        float elapsed = 0;
        while (elapsed < tiempo)
        {
            
            player.transform.position = Vector3.Lerp(inicio, fin, elapsed / tiempo);
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return  null;


    }

    


    void Update()
    {

    }
}
