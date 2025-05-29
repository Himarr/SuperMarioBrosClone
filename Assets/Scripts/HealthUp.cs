using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PowerUp
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.SetHealth(3);
        Destroy(gameObject);

        
        GameManager.Instance.AddScore(100); 
    }
}
