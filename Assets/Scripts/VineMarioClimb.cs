using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VineMarioClimb : MonoBehaviour
{
    public Player player;
    public Vine vine;

    public IEnumerator MarioClimb()
    {

        

        while (player.canMove == false)
        {
            player.transform.Translate(0, 0.1f, 0);
            

            break;
        }
     
        yield return null;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        

        if (collision.gameObject.CompareTag("Player") && vine.timer > 3.5f  && Input.GetKey(KeyCode.W))
        {
            Debug.Log("Mario sube la enredadera");

            StartCoroutine(MarioClimb());

            player.canMove = false;
            player.gravity = 0;
            player.GetComponent<Animator>().SetBool("isClimbing", true);
        }

        if (collision.gameObject.CompareTag("Player") && vine.timer > 3.5f && player.GetComponent<Animator>().GetBool("isClimbing") == true)
        {
            player.SetVelocityX(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.canMove = true;
        player.gravity = -40;
        player.GetComponent<Animator>().SetBool("isClimbing", false);
    }


}
