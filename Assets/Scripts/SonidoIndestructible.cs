using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoIndestructible : MonoBehaviour
{
    public static SonidoIndestructible Instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

    }

    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }

}
