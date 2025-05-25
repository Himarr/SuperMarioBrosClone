using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
    }

    public void GoToLoadScreen()
    {
        SceneManager.LoadScene("Load Screen");
    }

    public void GoToScreen1()
    {
        SceneManager.LoadScene("Test Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
