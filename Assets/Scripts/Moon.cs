using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddMoon();
            Destroy(gameObject);
        }
    }
}
