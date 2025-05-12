using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float platformVelocity;
    public bool goesUp;

    public float initialPositionX;
    public float initialPositionY;
    public float initialPositionZ;

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

        platformPositionChange();

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
}
