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

    bool isMoving;
    private int dir;


    void Start()
    {
        speed = 0;
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { isMoving = false; }
        HandleMovement();
    }

    private void FixedUpdate()
    {
        
        Debug.Log(dir);
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.D) && speed <= 0)
            // Velocidad inicial
        {
            speed = minSpeed;
            dir = 1;
        }
        if (Input.GetKey(KeyCode.D) && dir == 1)
            // Añadir aceleración
        {
            isMoving = true;

            if (speed <= maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            if (speed <= 0 && dir == -1)
            {
                speed = minSpeed;
                dir = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && speed <= 0)
        // Velocidad inicial
        {
            speed = minSpeed;
            dir = -1;
        }
        if (Input.GetKey(KeyCode.A) && dir == -1)
        // Añadir aceleración
        {
            isMoving = true;

            if (speed <= maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            if (speed <= 0 && dir == 1) 
            { 
                speed = minSpeed; 
                dir = -1;
            }
        }


        if (speed > 0 && !isMoving)
        {
            speed -= deceleration * Time.deltaTime;
        }

        if (speed < 0) { speed = 0; }

        // Mover
        this.transform.position += new Vector3(speed, 0) * Time.deltaTime * dir;
    }
}
