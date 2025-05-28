using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float acceleration = 30f;
    public float friction = 20f;
    public float gravity = -40f;
    public float jumpForce = 40f;
    public float holdJumpForce = 10f;
    public float direction;

    [SerializeField]
    private float currentVelocityX = 0f;
    [SerializeField]
    private float currentVelocityY = 0f;

    bool moveLeft;
    bool moveRight;
    bool moveUp;

    [Header("Collision")]
    public LayerMask collisionMask;
    public float skinWidth = 0.02f;
    
    // Variables iniciales
    bool isMoving;
    public bool isJumping;
    bool isGrounded = false;
    public bool canMove = true;
    bool isRunning;
    bool isCrouching;
    bool isShooting;
    bool isBraking;
    bool isInvincible;
    public int dir;

    public Rigidbody2D rb;
    Camera cam;
    public Animator anim;
    public BoxCollider2D col;
    public SpriteRenderer sprite;
    public GameObject fireBall;

    //Variables declarada por Pablo
    bool playerCanInput = true;
    public bool bounceOnEnenemy = false;

    // Estado de mario
    string[] status = {"small", "big", "fire", "star"};
    public string currentStatus;

    private void Awake()
    {
        currentStatus = "small";
        anim.SetBool("isSmall", true);
    }

    void Start()
    {
        isJumping = true;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { isMoving = false; }
        if (playerCanInput) { HandleInput(); }
        MoveCamera();
    }

    private void FixedUpdate()
    {
        HandlePhysics();
        HandleAnimations();
    }

    private void MoveCamera()
    {
        if(GameObject.Find("Main Camera"))
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (cam.transform.position.x < this.transform.position.x)
            {
                Vector3 pos = transform.position;
                float pixelsPerUnit = 16f; // Ajusta a tu PPU
                pos.x = Mathf.Round(pos.x * pixelsPerUnit) / pixelsPerUnit;
                pos.y = Mathf.Round(cam.transform.position.y * pixelsPerUnit) / pixelsPerUnit;
                cam.transform.position = new Vector3(pos.x, pos.y, -10);
            }
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    float minPosition = col.bounds.min.y;

    //    if (collision.GetContact(0).point.y < minPosition)
    //    {
    //        isGrounded = true;
    //        jumpForce = 0;
    //    }
    //    else
    //    {
    //        isGrounded = false;
    //        jumpForce = 0;
    //    }

    //    isJumping = !isGrounded;
    //    anim.SetBool("isJumping", isJumping);
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        Goomba goomba = collision.gameObject.GetComponent<Goomba>();
        if (goomba != null && currentVelocityY < 0)
        {
            goomba.goombaDead();

            AddVelocityY(15f);
            bounceOnEnenemy = true;
        } else
        {
            bounceOnEnenemy=false;
        }

        // Muerte de Mario por tocar un enemigo
        if (collision.gameObject.CompareTag("Enemy") && currentVelocityY >= 0f && bounceOnEnenemy == false && !isInvincible)
        {
            onHit();
            // TODO haz que caiga hacia abajo
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    isJumping = true;
    //}

    private void HandleInput()
    {
        {
            // Movimiento derecha
            if (Input.GetKeyDown(KeyCode.D)) moveRight = true;
            if (Input.GetKeyUp(KeyCode.D)) moveRight = false;

            // Movimiento izquierda
            if (Input.GetKeyDown(KeyCode.A)) moveLeft = true;
            if (Input.GetKeyUp(KeyCode.A)) moveLeft = false;

            // Correr
            if (Input.GetKeyDown(KeyCode.K)) { isRunning = true; speed *= 2f; }
            if (Input.GetKeyUp(KeyCode.K)) { isRunning = false; speed /= 2f; }

            // Salto
            if (Input.GetKeyDown(KeyCode.L) && !isJumping) { moveUp = true; currentVelocityY = jumpForce; }
            if (Input.GetKeyUp(KeyCode.L)) { moveUp = false; currentVelocityY /= 2; }

            // Agacharse
            if (Input.GetKeyDown(KeyCode.S) && currentStatus != "small") { isCrouching = true; ResetCollider(); }
            if (Input.GetKeyUp(KeyCode.S) && currentStatus != "small") { isCrouching = false; ExtendCollider(); }

            // Disparar
            if (Input.GetKeyDown(KeyCode.K) && currentStatus == "fire") { isShooting = true; }
            if (Input.GetKeyUp(KeyCode.K) && currentStatus == "fire") { isShooting = false; }
        }
    }

    private void HandlePhysics()
    {
        float target = 0f;

        if (moveRight) target += 1f;
        if (moveLeft) target -= 1f;

        // Aceleración o fricción
        if (target != 0)
        {
            float desiredVelocityX = target * speed;
            float accel = acceleration;

            // Aumenta la fuerza si va en dirección opuesta
            if (Mathf.Sign(desiredVelocityX) != Mathf.Sign(currentVelocityX) && currentVelocityX != 0)
            {
                accel *= 2f;
                isBraking = true;
            } else { isBraking = false; }

            currentVelocityX = Mathf.MoveTowards(currentVelocityX, desiredVelocityX, accel * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocityX = Mathf.MoveTowards(currentVelocityX, 0f, friction * Time.fixedDeltaTime);
        }

        // Gravedad
        if (isJumping)
        {
            currentVelocityY += gravity * Time.fixedDeltaTime;
        }

        // Saltar
        if (moveUp)
        {
            Jump();
        }

        // Disparar
        if (isShooting) { Shoot(); }

        Vector2 moveAmount = new Vector2(currentVelocityX, currentVelocityY) * Time.fixedDeltaTime;

        // BoxCast horizontal
        if (moveAmount.x != 0)
        {
            direction = Mathf.Sign(moveAmount.x);

            Vector2 originH = (Vector2)col.bounds.center + Vector2.right * direction * (col.bounds.extents.x - skinWidth);
            Vector2 boxSizeH;

            if (isJumping)
            {
                boxSizeH = new Vector2(skinWidth, col.bounds.size.y - skinWidth * 10);
            } else
            {
                boxSizeH = new Vector2(skinWidth, col.bounds.size.y - skinWidth * 2);
            }

            float castDistanceH = Mathf.Abs(moveAmount.x) + skinWidth;

            RaycastHit2D hit = Physics2D.BoxCast(originH, boxSizeH, 0f, Vector2.right * direction, castDistanceH, collisionMask);

            if (hit.collider != null)
            {
                float distanceToCollider = hit.distance - skinWidth;
                moveAmount.x = direction * Mathf.Min(Mathf.Abs(moveAmount.x), distanceToCollider);
                currentVelocityX = 0f;
            }
        }
        // BoxCast Vertical
        if (moveAmount.y != 0)
        {
            float directionY = Mathf.Sign(moveAmount.y);

            Vector2 originV = (Vector2)col.bounds.center + Vector2.up * directionY * (col.bounds.extents.y - skinWidth);

            Vector2 boxSizeV = new Vector2(col.bounds.size.x - skinWidth * 2, skinWidth);

            float verticalCastDistance = Mathf.Abs(moveAmount.x) + skinWidth * 2f;

            if (moveAmount.y > 0) { verticalCastDistance = skinWidth; }


            RaycastHit2D hit = Physics2D.BoxCast(originV, boxSizeV, 0f, Vector2.up * directionY, verticalCastDistance, collisionMask);

            if (hit.collider != null)
            {
                float distanceToCollider = hit.distance - skinWidth;
                moveAmount.y = directionY * Mathf.Min(Mathf.Abs(moveAmount.y), distanceToCollider);
                currentVelocityY /= 2f;

                if (currentVelocityY > -1f && moveAmount.y < 0) { currentVelocityY = 0f; }
                if (moveAmount.y > 0) { currentVelocityY = 0f; }
            }
        }

        Vector2 origin = (Vector2)col.bounds.center - new Vector2(0, col.bounds.extents.y - skinWidth);
        Vector2 boxSize = new Vector2(col.bounds.size.x - skinWidth * 2f, skinWidth);
        float castDistance = skinWidth * 5f;

        RaycastHit2D groundHit = Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, castDistance, collisionMask);

        bool grounded = groundHit.collider != null;
       
        isJumping = !grounded;

        rb.MovePosition(rb.position + moveAmount);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            currentVelocityY = jumpForce;
        } else if (currentVelocityY > 0)
        {
            currentVelocityY += holdJumpForce * Time.fixedDeltaTime;
        }
    }

    private void HandleAnimations()
    {
        if (moveRight) { sprite.flipX = false; }
        if (moveLeft) { sprite.flipX = true; }

        if (moveRight || moveLeft) { anim.SetBool("isMoving", true); }
        else { anim.SetBool("isMoving", false); }

        if (isRunning) { anim.SetBool("isRunning", isRunning); }
        else { anim.SetBool("isRunning", isRunning); }

        if (isBraking) { anim.SetBool("isBraking", isBraking); }
        else { anim.SetBool("isBraking", isBraking); }

        if (isJumping) { anim.SetBool("isJumping", isJumping); }
        else { anim.SetBool("isJumping", isJumping); }

        if (isCrouching) { anim.SetBool("isCrouching", isCrouching); }
        else { anim.SetBool("isCrouching", isCrouching); }

        if (isShooting) { anim.SetTrigger("isShooting"); }
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
        anim.SetTrigger("isShooting");
        isShooting = false;
        Instantiate(fireBall, new Vector3(transform.position.x + (0.2f * direction), transform.position.y + 0.25f), Quaternion.identity);
    }

    public float GetVelocityX()
    {
        return currentVelocityX;
    }

    public float GetVelocityY()
    {
        return currentVelocityY;
    }

    public void AddVelocityY(float amount)
    {
        currentVelocityY += amount;
    }
}
