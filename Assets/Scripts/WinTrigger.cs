using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    bool hasFiveMoons = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetMoons() >= 5 && hasFiveMoons == false)
        {
            hasFiveMoons = true;
            SceneManager.LoadScene("FinalOdyssey");
        }
        if (hasFiveMoons == true && collision.gameObject.CompareTag("Player") && GameManager.Instance.GetMoons() >= 8) 
        {
            SceneManager.LoadScene("FinalOdyssey 1");
        }
    }
}
