using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int score;
    int lives = 3;
    int coins = 0;
    string playerState;
    float timer = 400;

    int moons = 0;
    public HashSet<string> collectedMoons = new HashSet<string>();
    int health = 3;

    public bool playerOnScene = false;
    bool isPaused = false;

    public AudioClip pause, hurryUpOverW, songOverW, songUnderW, hurryUpU, gameOver, death;

    public AudioSource audioSource;

    public Player player;
    bool playerHasCappy;
    TestCappy cappy;
    public string currentSceneName;

    



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime * 3f;
        }
        else if (timer <= 0)
        {
            player.Die();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) { PauseGame(); }
            else { ResumeGame(); }

        }

        moons = collectedMoons.Count;
        DeathSounds();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        SceneMusic();

        player = FindAnyObjectByType<Player>();
        cappy = FindAnyObjectByType<TestCappy>();
        SetPlayerState();

        timer = 400;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddLives(int amount = 1)
    {
        lives += amount;
        Debug.Log(lives);
    }

    public int GetLives() { return lives; }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        Debug.Log(coins);

        // TODO - Si tiene 100 monedas transformar en una vida.
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddMoon(int amount = 1)
    {
        moons += amount;
        Debug.Log(moons);
    }

    public int GetMoons() { return moons; }

    public void SavePlayerState()
    {
        if (player != null)
        {
            playerState = player.currentStatus;
        }
        if (cappy != null)
        {
            playerHasCappy = cappy.GetCappy();
        }
    }

    public void SetPlayerState()
    {
        player = FindAnyObjectByType<Player>();
        cappy = FindAnyObjectByType<TestCappy>();

        if (playerState == "big")
        {
            player.anim.SetBool("isBig", true);
            player.anim.SetBool("isFire", false);
            player.anim.SetBool("isSmall", false);

            int animHash = Animator.StringToHash("Base Layer.Big Mario.BigMario_Idle");
            player.anim.Play(animHash);
            player.ExtendCollider();
            player.currentStatus = playerState;
        }
        else if (playerState == "fire")
        {
            player.anim.SetBool("isFire", true);
            player.anim.SetBool("isSmall", false);
            player.anim.SetBool("isBig", false);

            int animHash = Animator.StringToHash("Base Layer.Fire.Fire_Idle");
            player.anim.Play(animHash);

            player.ExtendCollider();
            player.currentStatus = playerState;
            Debug.Log(playerState);
        }

        if (cappy != null)
        {
            cappy.canThrowCappy = playerHasCappy;

            if (playerHasCappy)
            {
                cappy.GetComponent<Animator>().SetTrigger("HaveCappy");
            }

        }
    }

   
    public int GetTimer()
    {
        return Mathf.RoundToInt(timer);
    }

    void PauseGame()
    {
         StartCoroutine(PausePlay());
    }

    IEnumerator PausePlay()
    {
        audioSource.PlayOneShot(pause);
        isPaused = true;

        yield return new WaitForSeconds(0.691f);
        yield return StartCoroutine(PauseSounds());
    }

    IEnumerator PauseSounds()
    {
        
        audioSource.Pause();
        yield return null;

        Time.timeScale = 0;
    }

    IEnumerator ResumeSounds()
    {
        audioSource.UnPause();
        yield return null;
    }

    void ResumeGame()
    {
        StartCoroutine(ResumeSounds());
        Time.timeScale = 1;
        isPaused = false;
    }

    void DeathSounds()
    { 
        if (player.isAlive == false && player.deathTrigger == false)
        {
            player.deathTrigger = true;
             StartCoroutine(StopSounds());
        }
    }

    IEnumerator StopSounds()
    {
        audioSource.Stop();
        yield return StartCoroutine(DeathSound());
    }

    IEnumerator DeathSound()
    {
        audioSource.clip = death;
        audioSource.Play();
        yield return null;
    }


    public int GetHealth() { return health; }

    public void AddHealth(int amount) { health += amount; }

    public void SetHealth(int amount) { health = amount; }

    public void SceneMusic()
    {
        string[] scenes = currentSceneName.Split(' ');


        if (currentSceneName == "1-1")
        {
            audioSource.clip = songOverW;
            audioSource.Play();
            return;
        }

        if (currentSceneName == "1-2")
        {
            audioSource.clip = songUnderW;
            audioSource.Play();
            return;
        }

        if (currentSceneName == "1-3")
        {
            audioSource.clip = songOverW;
            audioSource.Play();
            return;
        }

        if (currentSceneName == "1-3 Undgr. 1")
        {
            audioSource.clip = songUnderW;
            audioSource.Play();
            return;
        }

        if (currentSceneName == "Death Screen")
        {
            audioSource.clip = gameOver;
            audioSource.Play();
            return;
        }

        if (scenes.Contains("Load") || currentSceneName == "Load" )
        {
            audioSource.Stop();
            return;
        }
    }

    public AudioSource GetAudio()
    {
        return audioSource;
    }
}
