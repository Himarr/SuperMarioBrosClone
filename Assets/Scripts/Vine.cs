using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Vine : MonoBehaviour
{

    public float vineVelocity;
    public float timer;

    public GameObject vinePiece;
    public GameObject vinePiece1;
    public GameObject vinePiece2;
    public GameObject vinePiece3;
    public GameObject vinePiece4;
    public GameObject vinePiece5;

    public IEnumerator VineMovement()
    {

        while(timer < 3.5){
            gameObject.transform.Translate(0, vineVelocity * Time.deltaTime, 0);

            if (timer > 0.5)
            {
                vinePiece.transform.parent = gameObject.transform;
            }
            if (timer > 1)
            {
                vinePiece1.transform.parent = gameObject.transform;
            }
            if (timer > 1.5)
            {
                vinePiece2.transform.parent = gameObject.transform;
            }
            if (timer > 2)
            {
                vinePiece3.transform.parent = gameObject.transform;
            }
            if (timer > 2.5)
            {
                vinePiece4.transform.parent = gameObject.transform;
            }
            if (timer > 3)
            {
                vinePiece5.transform.parent = gameObject.transform;
            }
            if (timer > 3.5)
            {
                vineVelocity = 0;
            }

            timer += Time.deltaTime;
            
             yield return null;
        }
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && collision.CompareTag("Player"))
        {

            StartCoroutine(VineMovement());
            
        }


    }
}
