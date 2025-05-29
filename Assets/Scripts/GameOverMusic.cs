using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Music : MonoBehaviour
{
    public AudioClip  hurryUpOverW, songOverW, songUnderW, hurryUpU;
    public AudioSource audioSource;

    public string currentSceneName;

    Player player;

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void Scene()
    {
        string[] scenes = currentSceneName.Split(' ');


        if (scenes.Contains("1-1") || currentSceneName == "1-1")
        {
            StartCoroutine(SceneLoader("Load 1-1"));
            return;
        }

        if (scenes.Contains("1-2") || currentSceneName == "1-2")
        {
            StartCoroutine(SceneLoader("Load 1-2"));
            return;
        }

        if (scenes.Contains("1-3") || currentSceneName == "1-3")
        {
            StartCoroutine(SceneLoader("Load 1-3"));
            return;
        }

        IEnumerator SceneLoader(string sceneName)
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(sceneName);
        }
    }

}


