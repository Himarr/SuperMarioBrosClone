using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Interface : MonoBehaviour
{
    //score 
    public Text scoreText;
    public Text coinText;
    public Text lifeText;
    public Text MoonText;


    public static Interface instance;

    private void Update()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString().PadLeft(6,'0'); 
        coinText.text = GameManager.Instance.GetCoins().ToString().PadLeft(2,'0');
        //lifeText.text = GameManager.Instance.AddLives()
    }

    
    
}
