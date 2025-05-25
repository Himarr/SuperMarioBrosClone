using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TCappy : MonoBehaviour
{

    public float initialPositionX;
    public float positionX;
    public float cappyDistance;

    void Start()
    {
        initialPositionX = transform.position.x; 
    }

    void Update()
    {
        positionX = transform.position.x;

        cappyDistance = initialPositionX - positionX;


        Debug.Log(cappyDistance);

        while ((cappyDistance > -4f))
        {
            gameObject.transform.Translate(0.02f, 0, 0);

            break;
        }

        transform.parent = null;
    }

   
}
