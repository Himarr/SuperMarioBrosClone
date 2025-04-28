using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    Player player;

    public float moveSpeed;
    public bool isMoving;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        player = GameObject.Find("Mario").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float contactY = collision.GetContact(0).point.y;
        float maxPosition = player.col.bounds.max.y;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + 0.5f);

        if (collision.gameObject.CompareTag("Player") && contactY > maxPosition && player.currentStatus == "small")
        {
            StartCoroutine(Mover(startPosition, endPosition, time));
        }

        //El siguiente if lo puso Pablo para que Mario grande rompa bloques
        if (collision.gameObject.CompareTag("Player") && contactY > maxPosition && (player.currentStatus == "big" || player.currentStatus == "fire"))
        {
            BlockBreak();
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
        while (elapsed < tiempo)
        {
            transform.position = Vector3.Lerp(inicio, fin, elapsed / tiempo);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = fin;
    }

    public void BlockBreak()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;



        Component[] rbs = GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D rb in rbs)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 4;

        }



        transform.Find("Bloques rotos_1").GetComponent<Rigidbody2D>().AddForce(new Vector2(-70f, 800f));
        Destroy(transform.Find("Bloques rotos_1").gameObject, 3f);

        transform.Find("Bloques rotos_2").GetComponent<Rigidbody2D>().AddForce(new Vector2(70f, 800f));
        Destroy(transform.Find("Bloques rotos_2").gameObject, 3f);

        transform.Find("Bloques rotos_3").GetComponent<Rigidbody2D>().AddForce(new Vector2(70f, 700f));
        Destroy(transform.Find("Bloques rotos_3").gameObject, 3f);

        transform.Find("Bloques rotos_4").GetComponent<Rigidbody2D>().AddForce(new Vector2(-70f, 700f));
        Destroy(transform.Find("Bloques rotos_4").gameObject, 3f);

        Destroy(gameObject, 3f);
    }
}

