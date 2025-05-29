using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    int health;
    int lastHealth;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        health = GameManager.Instance.GetHealth();
        lastHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        health = GameManager.Instance.GetHealth();

        if (health < lastHealth)
        {
            StartCoroutine(DisplayHealth());
            animator.SetTrigger("Hit");
        }
        else if (health > lastHealth)
        {
            StartCoroutine(DisplayHealth());
            animator.SetTrigger("Heal");
        }

        lastHealth = health;
    }

    IEnumerator DisplayHealth()
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(4);
        spriteRenderer.enabled = false;
    }
}
