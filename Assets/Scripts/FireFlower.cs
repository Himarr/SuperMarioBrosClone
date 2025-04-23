using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
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
            if (player.currentStatus == "small" || player.currentStatus == "big")
            {
                player.Grow("Fire");
                Debug.Log("colision");
            }
        }
        Destroy(gameObject);
    }
}
