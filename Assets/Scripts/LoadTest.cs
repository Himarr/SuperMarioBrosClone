using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTest : MonoBehaviour
{
    //cargar pantalla a los 3 segundos, poner el estado con el que se queda el jugador
    public string sceneName; 

    
    void Update()
    {
        StartCoroutine(Wait(3));    
    }

    private IEnumerator Wait(float time)
    {
        
        yield return new WaitForSeconds(time);
        Debug.Log("Han pasado 3 sec");
        SceneManager.LoadScene(sceneName);
    }

}
