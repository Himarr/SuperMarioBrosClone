using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int score;
    int lives = 3;
    int coins = 0;
    string playerState;

    public bool playerOnScene = false;

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
        
        // TODO - quitar spawnpoint
        GameObject spawnPoint = GameObject.Find("SpawnPoint");

        if (spawnPoint != null)
        {
            Destroy(spawnPoint);
            SetPlayerState();
        }
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void AddLives(int amount = 1)
    {
        lives += amount;
        Debug.Log(lives);
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        Debug.Log(coins);

        // TODO - Si tiene 100 monedas transformar en una vida.
    }

    public void SavePlayerState()
    {
        playerState = player.currentStatus;
    }

    public void SetPlayerState()
    {
        if (playerState == "big")
        {
            player.anim.SetBool("isBig", true);
            player.anim.SetBool("isFire", false);
            player.anim.SetBool("isSmall", false);

            int animHash = Animator.StringToHash("Base Layer.Big Mario.BigMario_Idle");
            player.anim.Play(animHash);
            player.ExtendCollider();
        }
        else if (playerState == "fire")
        {
            player.anim.SetBool("isFire", true);
            player.anim.SetBool("isSmall", false);
            player.anim.SetBool("isBig", false);

            int animHash = Animator.StringToHash("Base Layer.Fire.Fire_Idle");
            player.anim.Play(animHash);

            player.ExtendCollider();
        }
    }
}
