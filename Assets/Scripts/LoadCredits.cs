using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCredits : MonoBehaviour
{
    //creditos
    
    public string sceneName;

    void Update()
    {
        StartCoroutine(Wait(10));
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
