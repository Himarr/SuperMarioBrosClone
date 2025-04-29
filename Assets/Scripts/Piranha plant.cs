using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranhaplant : MonoBehaviour
{

    public float piranhaPlantVelocity;

    public float timer;

    public GameObject player;

    float distanciaConPlayer;
    public bool canMove = true;
  
    
    void Update()
    {
        if (canMove) { PiranhaMovement(); }

        PiranhaHides();
    }

    void PiranhaMovement()
    {
        timer += Time.deltaTime;


        gameObject.transform.Translate(0, piranhaPlantVelocity * Time.deltaTime, 0);


        if (timer > 0.67)
        {
            piranhaPlantVelocity = 0;
        }

        if (timer > 2)
        {
            piranhaPlantVelocity = -2;
        }

        if (timer > 2.675)
        {
            piranhaPlantVelocity = 0;
        }

        if (timer > 4.03)
        {
            timer = 0;
            piranhaPlantVelocity = 2;
        }
    }

    void PiranhaHides()
    {
        distanciaConPlayer = gameObject.transform.position.x - player.transform.position.x;

        if ((distanciaConPlayer < 2 && distanciaConPlayer > -2) && timer > 2.675)
        {
            canMove = false;

        } else { canMove = true; }
    }
}
