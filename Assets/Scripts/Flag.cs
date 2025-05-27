using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flag : MonoBehaviour
{
    Player player;
    BoxCollider2D col;
    Animator anim;

    public float moveSpeed;
    public float tiempo;
    public float coords = -3.5f;
    private float vel = 1;
    private GameObject flag;






    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
        anim = player.GetComponent<Animator>();
        flag = GameObject.FindWithTag("Flag");

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

        Vector3 inicioF = flag.transform.position;
        Vector3 finF = new Vector3(inicio.x, coords, inicio.z);

        float elapsed = 0;
        while (elapsed < tiempo)
        {
            flag.transform.position = Vector3.Lerp(inicioF, finF, elapsed / tiempo);
            player.transform.position = Vector3.Lerp(inicio, fin, elapsed / tiempo);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(MarioFlip()); ;


    }

    IEnumerator MarioFlip()
    {
        Vector3 inicio = player.transform.position;
        Vector3 fin = new Vector3(inicio.x += 0.9f, coords, inicio.z);
        float elapsed = 0;
        while (elapsed < tiempo / 2)
        {
            player.transform.position = Vector3.Lerp(inicio, fin, elapsed * (tiempo / 2));
            elapsed += Time.deltaTime;
            yield return player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        OnCollisionExit2D exit2D = null;
        anim.SetBool("isFlagDown", false);
        yield return StartCoroutine(MarioSuelta());
    }

    IEnumerator MarioSuelta()
    {
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        Vector3 inicio = player.transform.position;
        Vector3 fin = new Vector3(inicio.x + 0.25f, inicio.y, inicio.z);
        float elapsed = 0;
        while (elapsed < tiempo / 7)
        {
            player.transform.position = Vector3.Lerp(inicio, fin, elapsed / (tiempo / 7));
            elapsed += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("isMoving", true);
        yield return StartCoroutine(MarioMarcha());
    }

    IEnumerator MarioMarcha()
    {
        Vector3 inicio = player.transform.position;
        Vector3 fin = new Vector3(inicio.x, inicio.y - 0.8f, inicio.z);
        float elapsed = 0;
        while (elapsed < tiempo / 10)
        {
            player.transform.position = Vector3.Lerp(inicio, fin, elapsed / (tiempo / 10));
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(CaminarCastle());
    }

    IEnumerator CaminarCastle()
    {
        float duration = 2f; 
        float elapsed = 0;
        Vector3 inicio = player.transform.position;
        Vector3 fin = inicio + new Vector3(7f, 0, 0);

        player.canMove = false;
        anim.SetBool("isMoving", true);

        while (elapsed < duration)
        {
            player.transform.position = Vector3.Lerp(inicio, fin, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}