using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField]
    string moonId;

    public AudioClip moonSong;
    public AudioSource audioSource;
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
            AudioSource.PlayClipAtPoint(moonSong, gameObject.transform.position);
            GameManager.Instance.collectedMoons.Add(moonId);
            GameManager.Instance.AddScore(1000);
            Destroy(gameObject);
        }
    }
}
