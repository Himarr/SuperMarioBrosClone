using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Interface : MonoBehaviour
{
    //score 
    public static Interface instance; 

    public Text scoreText;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //scoreText.text = score.ToString() + "00000"; 
    }

    public void addScoreCoin()
    {
        score += 100;
        
        string Moneda = score.ToString().PadLeft(6,'0');
        scoreText.text = Moneda;

    }

    /*public void addScoreGoomba()
    { 
        score += 200;
        
        string puntosGoomba = score.ToString().PadLeft(6, '0');
        scoreText.text = puntosGoomba;

    }*/

}
