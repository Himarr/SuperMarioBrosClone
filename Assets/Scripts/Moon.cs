using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField]
    string moonId;
    private void Start()
    {
        if (GameManager.Instance.collectedMoons.Contains(moonId))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && moonId != null)
        {
            GameManager.Instance.collectedMoons.Add(moonId);
            GameManager.Instance.AddScore(1000);
            Destroy(gameObject);
        }
    }
}
