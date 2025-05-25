using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCappy : MonoBehaviour
{
    //Animator anim;
   
    void Start()
    {
        gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        CappyThrow();
    }

    public void CappyThrow()
    {
        if (Input.GetKey(KeyCode.K))
        {
            gameObject.GetComponent<Animator>().SetTrigger("DontHaveCappy");
        }
    }

}
