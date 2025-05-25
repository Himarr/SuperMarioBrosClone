using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public static string nextLevel;

    public static void LoadLevel(string level)
    {
        nextLevel = level; 
        SceneManager.LoadScene("LoadScreen");
    }
    

}
    

