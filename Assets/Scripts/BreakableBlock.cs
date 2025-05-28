using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BreakableBlock : MonoBehaviour
{
    Player player;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public Sprite underground;
    public Sprite overworld;

    public float moveSpeed;
    public float time;

    public bool canMove = true;

    public bool hasCoins;
    int coinAmount;

    public GameObject coin;

    public bool isOverworld;
     public AudioClip broken;
    public AudioClip moneda;
    public AudioSource audioSource;
    public AudioClip bump;

    void Start()
    {
        player = GameObject.Find("Mario").GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
        coinAmount = 10;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        if (isOverworld)
        {
            spriteRenderer.sprite = overworld;
            anim.SetBool("isOverworld", isOverworld);
        } else
        {
            spriteRenderer.sprite = underground;
            anim.SetBool("isOverworld", isOverworld);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float contactY = collision.GetContact(0).point.y;
        float maxPosition = player.col.bounds.max.y;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + 0.5f);

        if (collision.gameObject.CompareTag("Player")  && contactY > maxPosition && player.currentStatus == "small" || contactY > maxPosition && player.currentStatus =="big" && hasCoins ==true || contactY > maxPosition && player.currentStatus == "fire" && hasCoins == true)
        {
            if (canMove)
            {
                StartCoroutine(Mover(startPosition, endPosition, time, gameObject));

                if (hasCoins)
                {
                    StartCoroutine(SpawnCoin(startPosition, endPosition + new Vector3(0, 2), time));
                }
            }
            if (coinAmount == 0)
            {
                hasCoins = false;
                canMove = false;

                anim.SetBool("isLocked", true);
                AudioSource.PlayClipAtPoint(bump, gameObject.transform.position);
            }
        }

        //El siguiente if lo puso Pablo para que Mario grande rompa bloques
        if (collision.gameObject.CompareTag("Player") && contactY > maxPosition && (player.currentStatus == "big" && hasCoins == false|| contactY > maxPosition && player.currentStatus == "fire" && hasCoins == false))
        {
            BlockBreak();

            //puntos 
            GameManager.Instance.AddScore(50); 
        }

        //El siguiente if lo puso Pablo para que el koopa en caparazon rompa bloques
        Koopa koopa = collision.gameObject.GetComponent<Koopa>();
        if (koopa != null && koopa.tag == "KoopaInShell" && hasCoins == false)
        {
            BlockBreak();
        }

        //Cappy rompe bloques
        TCappy cappy = collision.gameObject.GetComponent<TCappy>();
        if (cappy != null && collision.gameObject.CompareTag("Cappy") && hasCoins == false)
        {
            BlockBreak();
        }

    }
    IEnumerator Mover(Vector3 startPosition, Vector3 endPosition, float time, GameObject gameObj)
    {
        // Subir
        canMove = false;
        yield return StartCoroutine(MoveObject(startPosition, endPosition, time, gameObject));
        // Bajar
        yield return StartCoroutine(MoveObject(endPosition, startPosition, time, gameObject));
        canMove = true;

        if (coinAmount == 0) { canMove = false; }
        
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

    public void BlockBreak()
    {
        AudioSource.PlayClipAtPoint(broken, gameObject.transform.position);
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
    IEnumerator SpawnCoin(Vector3 startPosition, Vector3 endPosition, float time)
    {
        if (coin != null)
        {
            GameObject coinObject = Instantiate(coin, startPosition, Quaternion.identity).gameObject;
            yield return MoveObject(startPosition, endPosition, time, coinObject);
            GameManager.Instance.AddCoins();
            GameManager.Instance.AddScore(100);
            coinAmount--;
            AudioSource.PlayClipAtPoint(moneda, gameObject.transform.position);
            
            Destroy(coinObject);
        }
    }
}

