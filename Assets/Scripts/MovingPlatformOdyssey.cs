using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformOdyssey : MonoBehaviour
{
    public float platformVelocity;

    public bool goesRight;

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
        if (player != null && goesRight == false)
        {
            PlatformMovement();
        }

        if (player != null && goesRight)
        {
            platformMovementRight();
        }
    }

    public void PlatformMovement()
    {
        gameObject.transform.Translate(0, platformVelocity * Time.deltaTime, 0);
    }

    public void platformMovementRight()
    {
        gameObject.transform.Translate(platformVelocity * Time.deltaTime, 0, 0);
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
