using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCappy : MonoBehaviour
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
   
    public AudioClip thorwCappy;
    public AudioSource audioSource;
    void Start()
    {
        gameObject.GetComponent<Animator>();
        
    }

    
    void Update()
    {
        if (canThrowCappy) 
        {
            CappyThrew();
        }
        
    }

    public void CappyThrew()
    {
        if (Input.GetKeyDown(KeyCode.K) && Time.time >= nextShot && player.direction == 1)
        {
            audioSource.PlayOneShot(thorwCappy);
            gameObject.GetComponent<Animator>().SetTrigger("DontHaveCappy");
            GameObject currentCappy = Instantiate(Cappy, Right.transform);
            nextShot = Time.time + 2;
            Destroy(currentCappy, 2);

            StartCoroutine(WaitAndGetCappy(2f));

        }else if (Input.GetKeyDown(KeyCode.K) && Time.time >= nextShot && player.direction == -1)
        {
            audioSource.PlayOneShot(thorwCappy);
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

    public bool GetCappy()
    {
        return canThrowCappy;
    }

}
