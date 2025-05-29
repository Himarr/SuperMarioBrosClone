using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameOverMusic : MonoBehaviour
{
    public AudioClip  hurryUpOverW, songOverW, songUnderW, hurryUpU;
    public AudioSource audioSource;

    public string currentSceneName;

    Player player;

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        Scene();
    }

    public void Scene()
    {
        string[] scenes = currentSceneName.Split(' ');


        if (scenes.Contains("1-1") || currentSceneName == "1-1")
        {
            audioSource.PlayOneShot(songOverW);
            Debug.Log("sonido sonidete chambal");
            return;
        }

        if (scenes.Contains("1-2") || currentSceneName == "1-2")
        {
            audioSource.PlayOneShot(songUnderW);
            return;
        }

        if (scenes.Contains("1-3 Undgr") || currentSceneName == "1-3 Undgr")
        {
            return;
        }
    }

}


