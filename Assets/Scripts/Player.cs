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

    public float jumpForce;
    public float holdJumpForce;
    public float maxJumpForce;
    public float initialJumpForce;
    bool isJumping;
    public Rigidbody2D rb;


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            isJumping = false;
            jumpForce = initialJumpForce;
        }
    }

    private void HandleMovement()
    {
        /* 
            Se encarga del Input y los cálculos de velocidad y aceleración de Mario.
        */
        if (Input.GetKeyDown(KeyCode.D) && speed <= 0)
            // Velocidad inicial
        {
            speed = minSpeed;
            dir = 1;
        }
        if (Input.GetKey(KeyCode.D))
            // Añadir aceleración
        {
            if (speed <= 0 && dir == -1) { dir = 1; speed = minSpeed; }

            if (dir == 1) { isMoving = true; }

            if (speed <= maxSpeed && dir == 1)
            {
                speed += acceleration * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && speed <= 0)
        // Velocidad inicial
        {
            speed = minSpeed;
            dir = -1;
        }
        if (Input.GetKey(KeyCode.A))
        // Añadir aceleración
        {
            if (speed <= 0 && dir == 1) { dir = -1; speed = minSpeed; }

            if (dir == -1) { isMoving = true; }

            if (speed <= maxSpeed && dir == -1)
            {
                speed += acceleration * Time.deltaTime;
            }
        }

        if (speed > 0 && !isMoving)
        {
            speed -= deceleration * Time.deltaTime;
        }

        if (speed < 0) { speed = 0; }

        // Mover
        this.transform.position += new Vector3(speed, 0) * Time.deltaTime * dir;

        // Saltar
        if (Input.GetKeyDown(KeyCode.L) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (isJumping && Input.GetKey(KeyCode.L) && jumpForce < maxJumpForce && rb.velocity.y > 0) 
        {
            rb.AddForce(new Vector2(0, holdJumpForce), ForceMode2D.Impulse);
            jumpForce += holdJumpForce;
        }
        //if (isJumping && Input.GetKeyUp(KeyCode.L)) { canJump = false; }

        // Correr
        if (Input.GetKeyDown(KeyCode.K) && !isJumping)
        {
            maxSpeed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            maxSpeed /= 2;
        }

        if (speed > maxSpeed)
        {
            speed -= deceleration * Time.deltaTime;
        }
    }
}
