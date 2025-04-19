using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    GameObject playerObject;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Mario");
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambiar estado de Mario
            if (player.currentStatus == "small")
            {
                player.currentStatus = "big";
                player.transform.position += new Vector3(0, 1);
                Destroy(gameObject);

                player.anim.SetTrigger("Big");
                player.col.size = new Vector2(1, player.col.size.y * 2);
            }
        }
        
    }
}
