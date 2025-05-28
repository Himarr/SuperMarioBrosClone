using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Mario"))
        {
            player = GameObject.Find("Mario").GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(player != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player.Die();
            }
        }
    }
}
