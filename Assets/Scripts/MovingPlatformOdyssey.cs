using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformOdyssey : MonoBehaviour
{
    public float platformVelocity; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (gameObject.transform.position.y >= 9)
        {
            Destroy(gameObject); 
        } 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlatformMovement();
        }
    }

    public void PlatformMovement()
    {
        gameObject.transform.Translate(0, platformVelocity * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.transform.parent = null;
        }

    }
}
