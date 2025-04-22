using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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
    bool canMove = true;
    private int dir;

    public float jumpForce;
    public float holdJumpForce;
    public float maxJumpForce;
    public float initialJumpForce;

    public Rigidbody2D rb;
    public Camera cam;
    public Animator anim;
    public BoxCollider2D col;
    public SpriteRenderer sprite;

    //Variables declarada por Pablo
    bool playerCanInput = true;
    bool bounceOnEnenemy = false;

    // Estado de mario
    string[] status = {"small", "big", "fire", "star"};
    public string currentStatus;


    void Start()
    {
        speed = 0;
        isJumping = true;
        jumpForce = 0;
        currentStatus = "small";
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { isMoving = false; }
        if (playerCanInput) { HandleMovement(); }
        MoveCamera(cam);
    }

    private void FixedUpdate()
    {
        StopMovement();
    }

    private void MoveCamera(Camera cam)
    {
        if (cam.transform.position.x < this.transform.position.x)
        {
            cam.transform.position = new Vector3(this.transform.position.x, 0.5f, -10);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float minPosition = col.bounds.min.y;

        if (collision.GetContact(0).point.y < minPosition)
        {
            isGrounded = true;
            jumpForce = 0;
        }
        else
        {
            isGrounded = false;
            jumpForce = 0;
        }

        isJumping = !isGrounded;
        anim.SetBool("isJumping", isJumping);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
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
        if (canMove)
        {
            this.transform.position += new Vector3(speed, 0) * Time.deltaTime * dir;
        }
        
        // Saltar
        if (Input.GetKey(KeyCode.L) && isGrounded)
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
            jumpForce = initialJumpForce;
        }
        if(Input.GetKeyUp(KeyCode.L))
        {
            jumpForce /= 2;
        }
        if (jumpForce < -8) { jumpForce = -8; }

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

    private void StopMovement()
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        float minYPosition = col.bounds.min.y + 0.2f;
        float maxYPosition = col.bounds.max.y;
        Vector2 lowerRay = new Vector2(col.bounds.center.x, minYPosition);
        Vector2 higherRay = new Vector2(col.bounds.center.x, maxYPosition);
        Vector2 rayDir = new Vector2(dir, 0);
        float distance = 0.5f;

        if (Physics2D.Raycast(lowerRay, rayDir, distance, mask) || Physics2D.Raycast(higherRay, rayDir, distance, mask))
        {
            Debug.Log("Hit");
            canMove = false;
            speed = 0;
            StartCoroutine(Wait(0.25f));
        }
        
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }




    //COSAS PUESTAS POR PABLO
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Goomba goomba = collision.gameObject.GetComponent<Goomba>();
        if (goomba != null && jumpForce < 0)
        {
            goomba.goombaDead();


            jumpForce += 10;
            bounceOnEnenemy = true;
        }

        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Breakable"))
        {
            bounceOnEnenemy = false;
        }

        //Muerte de Mario por tocar un enemigo
        if (collision.gameObject.CompareTag("Enemy") && jumpForce >= 0f && currentStatus == "small" && bounceOnEnenemy == false)
        {
            playerCanInput = false;
            speed = 0;
            gameObject.GetComponent<Animator>().SetBool("IsDead", true);
            gameObject.layer = LayerMask.NameToLayer("NoColission");

            //También haz que caiga hacia abajo, con el cambio de capa ya puede atravesar el suelo al morir, que yo no se hacerlo ahora
        }
    }
}
