using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreaks : MonoBehaviour
{

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
