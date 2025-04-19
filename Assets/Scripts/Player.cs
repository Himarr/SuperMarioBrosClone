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
    bool isJumping;
    bool isGrounded;
    private int dir;

    public float jumpForce;
    public float holdJumpForce;
    public float maxJumpForce;
    public float initialJumpForce;


    public Rigidbody2D rb;
    public Camera cam;
    public Animator anim;
    public Collider2D col;
    public SpriteRenderer sprite;


    void Start()
    {
        speed = 0;
        isJumping = true;
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { isMoving = false; }
        HandleMovement();
        MoveCamera(cam);
    }

    private void MoveCamera(Camera cam)
    {
        if (cam.transform.position.x < this.transform.position.x)
        {
            cam.transform.position = new Vector3(this.transform.position.x, 0.5f, -10);
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Block"))
            return;

        float minPosition = col.bounds.min.y;
        bool grounded = false;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.point.y < minPosition + 0.05f) // tolerancia para no fallar por un píxel
            {
                grounded = true;
                break;
            }
        }

        isGrounded = grounded;
        isJumping = !grounded;
        anim.SetBool("isJumping", !grounded);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            isGrounded = false;
            Debug.Log(isGrounded);
        }
    }

    private void HandleMovement()
    {
        /* 
            Se encarga del Input y los cálculos de velocidad y aceleración de Mario.
        */

        // Idle
        if (speed == 0) 
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isBraking", false);
        }

        if (Input.GetKeyDown(KeyCode.D) && speed <= 0)
            // Velocidad inicial
        {
            speed = minSpeed;
            dir = 1;
            Debug.Log("d");
        }
        if (Input.GetKey(KeyCode.D))
        // Añadir aceleración
        {
            if (speed <= 0 && dir == -1) { dir = 1; speed = minSpeed; }

            if (dir == 1)
            {
                isMoving = true;
                anim.SetBool("isMoving", true);
                sprite.flipX = false;
            }

            if (speed <= maxSpeed && dir == 1)
            {
                speed += acceleration * Time.deltaTime;
            }

            // Frenar
            if (speed <= maxSpeed && dir != 1)
            {
                speed -= deceleration * Time.deltaTime;
                anim.SetBool("isBraking", true);
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

            if (dir == -1)
            { 
                isMoving = true; 
                anim.SetBool("isMoving", true);
                sprite.flipX = true;
            }

            if (speed <= maxSpeed && dir == -1)
            {
                speed += acceleration * Time.deltaTime;
            }

            // Frenar
            if (speed <= maxSpeed && dir != -1)
            {
                speed -= deceleration * Time.deltaTime;
                anim.SetBool("isBraking", true);
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
        if (Input.GetKey(KeyCode.L) && isGrounded)
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
            jumpForce = initialJumpForce;
            Debug.Log("Juimp");
        }
        if(Input.GetKeyUp(KeyCode.L))
        {
            jumpForce /= 2;
        }

        if (isJumping || !isGrounded)
        {
            transform.position += new Vector3(0, jumpForce) * Time.deltaTime;
            jumpForce += gravity * Time.deltaTime;
        }

        // Correr
        if (Input.GetKeyDown(KeyCode.K))
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
