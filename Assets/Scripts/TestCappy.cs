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

    public Player player;

    public float nextShot;

    public bool canThrowCappy;
   
    void Start()
    {
        gameObject.GetComponent<Animator>();
        
    }

    
    void Update()
    {
        if (canThrowCappy) 
        {
            CappyThrow();
        }
        
    }

    public void CappyThrow()
    {
        if (Input.GetKeyDown(KeyCode.K) && Time.time >= nextShot && player.direction == 1)
        {
            gameObject.GetComponent<Animator>().SetTrigger("DontHaveCappy");
            GameObject currentCappy = Instantiate(Cappy, Right.transform);
            nextShot = Time.time + 2;
            Destroy(currentCappy, 2);

            StartCoroutine(WaitAndGetCappy(2f));

        }else if (Input.GetKeyDown(KeyCode.K) && Time.time >= nextShot && player.direction == -1)
        {
            gameObject.GetComponent<Animator>().SetTrigger("DontHaveCappy");
            GameObject currentCappy = Instantiate(Cappy, Left.transform);
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

    public void CappyGet()
    {
        canThrowCappy = true;
        gameObject.GetComponent<Animator>().SetTrigger("HaveCappy");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TCappyOnFloor cappyOnFloor = collision.gameObject.GetComponent<TCappyOnFloor>();
        if (cappyOnFloor != null)
        {
            CappyGet();

        }
        
    }

}
