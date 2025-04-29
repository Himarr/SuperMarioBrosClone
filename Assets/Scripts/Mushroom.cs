using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.UI;

public class Mushroom : PowerUp
{
    float speed = 3;
    BoxCollider2D col;
    void Start()
    {
        Player = GameObject.Find("Mario").GetComponent<Player>();
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
    }

    private void FixedUpdate()
    {
        ForwardRaycast();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambiar estado de Mario
            if (Player.currentStatus == "small")
            {
                Player.Grow("Big");
            }
            Destroy(gameObject);
        }
    }

    private void ForwardRaycast()
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        Vector2 ray = col.bounds.center;
        int dir;
        if (speed > 0) dir = 1;
        else dir = -1;
        Vector2 rayDir = new Vector2(dir, 0);
        float distance = 0.6f;

        if (Physics2D.Raycast(ray, rayDir, distance, mask))
        {
            speed *= -1;
        }
    }
}
