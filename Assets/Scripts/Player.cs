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
    bool isCrouching;
    bool isInvincible;
    public int dir;

    public float jumpForce;
    public float holdJumpForce;
    public float maxJumpForce;
    public float initialJumpForce;

    public Rigidbody2D rb;
    Camera cam;
    public Animator anim;
    public BoxCollider2D col;
    public SpriteRenderer sprite;
    public GameObject fireBall;

    //Variables declarada por Pablo
    bool playerCanInput = true;
    bool bounceOnEnenemy = false;

    // Estado de mario
    string[] status = {"small", "big", "fire", "star"};
    public string currentStatus;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentStatus = "small";
        anim.SetBool("isSmall", true);
    }

    void Start()
    {
        speed = 0;
        isJumping = true;
        jumpForce = 0;

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

            if (speed <= maxSpeed && dir == 1 && !isCrouching)
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

            if (speed <= maxSpeed && dir == -1 && !isCrouching)
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

        if (speed > 0 && !isMoving || isCrouching)
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
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            maxSpeed /= 2;
            anim.SetBool("isRunning", false);
        }

        if (speed > maxSpeed)
        {
            speed -= deceleration * Time.deltaTime;
        }

        // Agacharse
        if (currentStatus != "small")
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isCrouching = true;
                anim.SetBool("isCrouching", isCrouching);
                ResetCollider();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                isCrouching = false;
                anim.SetBool("isCrouching", isCrouching);
                ExtendCollider();
            }
        }

        // Disparar
        if (currentStatus == "fire" && Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
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
        float distanceBig = 0.6f;

        if ((Physics2D.Raycast(lowerRay, rayDir, distance, mask) || Physics2D.Raycast(higherRay, rayDir, distance, mask)) && currentStatus == "small")
        {
            Debug.Log("Hit");
            canMove = false;
            speed = 0;
            StartCoroutine(Wait(0.25f));
        } 
        else if (currentStatus != "small" && Physics2D.Raycast(lowerRay, rayDir, distanceBig, mask) || Physics2D.Raycast(higherRay, rayDir, distanceBig, mask) || Physics2D.Raycast(col.bounds.center, rayDir, distanceBig, mask))
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
        speed = 0;
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
        if (collision.gameObject.CompareTag("Enemy") && jumpForce >= 0f && bounceOnEnenemy == false && !isInvincible)
        {
            onHit();
            //También haz que caiga hacia abajo, con el cambio de capa ya puede atravesar el suelo al morir, que yo no se hacerlo ahora
        }
    }

    public void Grow(string trigger)
    {
        StartCoroutine(GrowCoroutine(trigger));
    }

    private IEnumerator GrowCoroutine(string trigger)
    {
        // Corrutina que maneja los cambios en el collider durante la animación de PowerUp.

        anim.SetTrigger(trigger);
        
        canMove = false;
        isInvincible = true;

        if (currentStatus != "small") { ResetCollider(); }

        yield return new WaitForSeconds(0.4f);

        if (currentStatus != "small") { ExtendCollider(); }

        if (trigger == "Big")
        {
            anim.SetBool("isBig", true);
            anim.SetBool("isSmall", false);
            ExtendCollider();
            currentStatus = "big";
        }
        else if (trigger == "Hit")
        {
            anim.SetBool("isSmall", true);
            anim.SetBool("isBig", false);
            anim.SetBool("isFire", false);
            ResetCollider();
            currentStatus = "small";
        }
        else if (trigger == "Fire")
        {
            anim.SetBool("isFire", true);
            if (currentStatus == "small")
            {
                ExtendCollider();
                
                Debug.Log("fuego a la cachimba");
            } 
            
            anim.SetBool("isSmall", false);
            anim.SetBool("isBig", false);
            currentStatus = "fire";
        }

        canMove = true;
        Debug.Log(isInvincible);
        yield return new WaitForSeconds(1f);
        isInvincible = false;
        Debug.Log(isInvincible);
    }
    public void ExtendCollider()
    {
        // Extiende el collider de Mario a su versión grande.

        transform.position += new Vector3(0, 0.5f);
        col.size = new Vector2(1, col.size.y * 2);
        canMove = true;
        Debug.Log("ta grande");
    }

    public void ResetCollider()
    {
        // Devuelve el collider a su tamaño original.

        transform.position -= new Vector3(0, 0.5f);
        col.size = new Vector2(0.75f, 0.95f);
        canMove = true;
        Debug.Log("chikito");
    }

    public void onHit()
    {
        if (currentStatus == "small")
        {
            // Die
            playerCanInput = false;
            speed = 0;
            gameObject.GetComponent<Animator>().SetBool("IsDead", true);
            gameObject.layer = LayerMask.NameToLayer("NoColission");

            // TODO - Hacer que caiga
        }
        else if (currentStatus == "big" || currentStatus == "fire")
        {
            // Hacer chikito
            Grow("Hit");
        }
        else if (currentStatus == "star")
        {
            // Hacer invulnerable
        }
    }

    private void Shoot()
    {
        Debug.Log("Pium Pium");
        Instantiate(fireBall, new Vector3(transform.position.x + (0.2f * dir), transform.position.y + 0.25f), Quaternion.identity);
    }
}
