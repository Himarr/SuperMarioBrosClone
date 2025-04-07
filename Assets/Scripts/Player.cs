using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables iniciales

    public float gravity;
    public float speed;
    public float acceleration;
    public float maxSpeed;
    public float minSpeed;
    public float deceleration;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.D) && speed == 0)
            // Velocidad inicial
        {
            speed = minSpeed;
        }
        if (Input.GetKey(KeyCode.D))
            // Añadir aceleración
        {
            if (speed <= maxSpeed)
            {
                speed += acceleration;
            }
            

            this.transform.position += new Vector3(speed, 0) * Time.deltaTime;
        }
        
    }
}
