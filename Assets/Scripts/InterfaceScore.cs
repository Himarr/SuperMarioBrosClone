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
        //scoreText.text = score.ToString() + "00000"; NO HACE FALTA 
    }

    public void addScoreCoin()
    {
        score += 100;
        
        string Moneda = score.ToString().PadLeft(6,'0');
        scoreText.text = Moneda;

    }

    public void addScoreGoomba()
    { 
        score += 100;
        
        string puntosGoomba = score.ToString().PadLeft(6, '0');
        scoreText.text = puntosGoomba;

    }

    public void addScoreKoopa()
    {
        score += 200;

        string puntosKoopa = score.ToString().PadLeft(6, '0');
        scoreText.text = puntosKoopa;

        Interface.instance.addScoreKoopa();

    }

    public void addScoreMushroom()
    {
        score += 1000;

        string puntosMushroom = score.ToString().PadLeft(6, '0');
        scoreText.text = puntosMushroom;

    }
    public void addScoreFlower()
    {
        score += 1000;

        string puntosFlower = score.ToString().PadLeft(6, '0');
        scoreText.text = puntosFlower;

    }

}
