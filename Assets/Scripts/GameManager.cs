using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int score;
    int lives = 2;
    int coins = 0;
    string playerState;
    float timer = 400;

    int moons = 0;
    int life = 3;

    public bool playerOnScene = false;
    bool isPaused = false;

    public AudioClip pause;
    public AudioSource audioSource;

    public Player player;
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
        } else if (timer <= 0)
        {
            player.Die();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) { PauseGame(); }
            else { ResumeGame(); }
            
        }
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
        player = FindAnyObjectByType<Player>();
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


    public void SavePlayerState()
    {
        if (player != null)
        {
           playerState = player.currentStatus;
        }
    }

    public void SetPlayerState()
    {
        player = FindAnyObjectByType<Player>();

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
    }

    public int GetTimer()
    {
        return Mathf.RoundToInt(timer);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        audioSource.PlayOneShot(pause);
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
