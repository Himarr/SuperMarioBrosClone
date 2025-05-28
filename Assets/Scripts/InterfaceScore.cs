using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class Interface : MonoBehaviour
{
    //score 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI MoonText;

    private void Update()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString().PadLeft(6,'0'); 
        coinText.text = GameManager.Instance.GetCoins().ToString().PadLeft(2,'0');
        timerText.text = GameManager.Instance.GetTimer().ToString().PadLeft(3,'0');
        
        if (lifeText != null) { lifeText.text = GameManager.Instance.GetLives().ToString(); }
    }


    void LateUpdate()
    {
        Vector3 pos = transform.position;
        float pixelsPerUnit = 16f;
        pos.x = Mathf.Round(pos.x * pixelsPerUnit) / pixelsPerUnit;
        pos.y = Mathf.Round(pos.y * pixelsPerUnit) / pixelsPerUnit;
        transform.position = pos;
    }
}
