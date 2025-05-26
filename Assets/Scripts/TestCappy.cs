using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCappy : MonoBehaviour
{
    [SerializeField]
    GameObject Cappy;

    [SerializeField]
    Transform Right;
    [SerializeField]
    Transform Left;

    public float nextShot;
   
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
        if (Input.GetKeyDown(KeyCode.K) && Time.time >= nextShot)
        {
            gameObject.GetComponent<Animator>().SetTrigger("DontHaveCappy");
            GameObject currentCappy = Instantiate(Cappy, Right.transform);
            nextShot = Time.time + 2;
            Destroy(currentCappy, 2);

            StartCoroutine(WaitAndGetCappy(2f));

        }
    }
    private IEnumerator WaitAndGetCappy(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Animator>().SetTrigger("HaveCappy");

    }


    

    
}
