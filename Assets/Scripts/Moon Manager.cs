using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : MonoBehaviour
{
    public GameObject currentMoons;
    int moons;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moons = GameManager.Instance.GetMoons();
        if (moons == 0)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        if (moons == 1)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        if (moons == 2)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        if (moons == 3)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        if (moons == 4)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[4];
        }
        if (moons == 5)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[5];
        }
        if (moons == 6)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[6];
        }
        if (moons == 7)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[7];
        }
        if (moons == 8)
        {
            currentMoons.GetComponent<SpriteRenderer>().sprite = sprites[8];
        }
    }
}
