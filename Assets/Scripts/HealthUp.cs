using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PowerUp
{
    public AudioClip  health;
    public AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(health, gameObject.transform.position);
        GameManager.Instance.SetHealth(3);
        Destroy(gameObject);
    }
}
