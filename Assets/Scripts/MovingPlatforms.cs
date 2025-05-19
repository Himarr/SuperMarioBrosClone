using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovingPlatforms : MonoBehaviour
{
    public float platformVelocity;
    public bool goesUp;

    public float initialPositionX;
    public float initialPositionY;
    public float initialPositionZ;

    public bool odysseyCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        initialPositionX = transform.position.x;
        initialPositionY = transform.position.y;
        initialPositionZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        platformMovement();

        if (odysseyCoordinates == false)
        {
            platformPositionChange();
        }
   
        if (odysseyCoordinates)
        {
            platformPositionChangeOdyssey();
        }

    }
    
    void platformMovement()
    {
        if (goesUp)
        {
            gameObject.transform.Translate(0, platformVelocity * Time.deltaTime, 0);
        }
        else
        {
            gameObject.transform.Translate(0, platformVelocity * Time.deltaTime, 0);
        }
    }

    void platformPositionChange()
    {
        if (gameObject.transform.position.y > -6.6f && goesUp == true)
        {
            gameObject.transform.position = new Vector2(initialPositionX, -22.5f);
        }

        if (gameObject.transform.position.y < -23f && goesUp == false)
        {
            gameObject.transform.position = new Vector2(initialPositionX, -6.2f);
        }
    }

    void platformPositionChangeOdyssey()
    {
        if(gameObject.transform.position.y > 8.5f && goesUp == true)
        {
            gameObject.transform.position = new Vector2(initialPositionX, -7.6f);
        }

        if (gameObject.transform.position.y < -7.6f && goesUp == false)
        {
            gameObject.transform.position = new Vector2(initialPositionX, 8.5f);
        }
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
