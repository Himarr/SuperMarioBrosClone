using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    //Empezar en escena 0 (menu) 
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0); 
    }

    //Saltar a pantalla de carga tras darle a Classic Mode  
    public void GoToLoadScreen()
    {
        SceneManager.LoadScene("Load 1-1"); 
    }

    //Ir a pantalla de carga tras darle a Odyssey Mode (Mario sin gorra) 
    public void GoToLoadScreenOdyssey()
    {
        SceneManager.LoadScene("Load 1-3");
    }

    //Salir del juego 
    public void QuitGame()
    {
        Application.Quit();
    }
}
