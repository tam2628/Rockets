using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameStates { Default, Started, Paused, Ended, Resumed };
    public GamedataScriptableObject gameData;
    [SerializeField]
    private int scorePoints, coinPoints, hitDamage;
    private GameObject player;
    [HideInInspector]
    public int playerScore, playerCoins, playerHealth = 10;
    [SerializeField]
    private List<Character> characters;
    [SerializeField]
    private string defaultCharacterId;
    private Material playerBackgroundMaterial;
    [SerializeField]
    private GameObject shopPrefab;
    private Vector3 playerPosition = new Vector3(0, -2.3f, 10), 
        playerRotation = new Vector3(-90, 0, 0);
    private Queue<PowerUp> powerUpQueue;

    public static event Action GameResumed;

    public void IncreasePlayerHealth(int healthPoints) => playerHealth += healthPoints;

    private void Awake()
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);

        string characterId = PlayerPrefs.GetString("character", defaultCharacterId);
        foreach (Character c in characters)
        { 
            if (c.id == characterId)
            {
                player = Instantiate(c.characterPrefab);
                player.transform.position = playerPosition;
                player.transform.rotation = Quaternion.Euler(playerRotation);
                playerBackgroundMaterial = c.charcterBackgroundMaterial;
            }
        }
    }

    public Material GetPlayerBackgroundMaterial() => playerBackgroundMaterial;

    public void GameScreenCountdownOver()
    {
        gameData.gameState = GameStates.Started;
    }

    private void Start()
    {
        powerUpQueue = new Queue<PowerUp>();
        gameData.gameState = GameStates.Default;
        StartMenuUI.PlayButtonPressed += () =>
        {
            SceneManager.LoadScene(1);
            gameData.gameState = GameStates.Paused;        
        };
        Player.PlayerScored += () => playerScore += scorePoints;
        Player.PlayerCollectedCoin += () => playerCoins += coinPoints;
        Player.PlayerHit += () =>
        {
            playerHealth -= hitDamage;
            isPlayerDead = playerHealth <= 0 ? true : false;
        };
        Player.PlayerSaved += () =>
        {
            playerHealth = 10;
            //gameData.gameState = GameStates.Started;
            player.SetActive(true);
        };
        EndgameCanvasScript.GotoMainMenuButtonPressed += () =>
        {
            gameData.playerHighScore = playerScore > gameData.playerHighScore ? playerScore : gameData.playerHighScore;
            gameData.playerTotalCoins += playerCoins;
            SceneManager.LoadScene(0);
            playerHealth = 10;
            gameData.gameState = GameStates.Default;
        };

        EndgameCanvasScript.ShowRewardedAdButtonPressed += () => gameData.gameState = GameStates.Paused;

        AdsManager.RewardedVideoAdFinished += () =>
        {
            EndgameCanvasScript.instance.HideEndGameScreen();
            GameCanvasScript.instance.gameObject.SetActive(true);
            GameResumed?.Invoke();
        };
        Player.PlayerDed += () =>
        {
            while(powerUpQueue.Count != 0)
                powerUpQueue.Dequeue().UsePowerUp();
            EndgameCanvasScript.instance.ShowEndGameScreen();
            GameCanvasScript.instance.gameObject.SetActive(false);
        };
        Shop.ShopItemBought += (int price) => gameData.playerTotalCoins -= price;
       
        playerScore = playerCoins = 0;
    }

    private bool isPlayerDead = false;
    public bool IsPlayerDed() => playerHealth <= 0 ? true : false;

    public bool PlayerHasEnoughMoney(int price) => gameData.playerTotalCoins >= price;

    private void Update()
    {
        if (gameData.gameState == GameStates.Default)
        {
            playerScore = playerCoins = 0;
            player.transform.position = playerPosition;
            player.transform.rotation = Quaternion.Euler(playerRotation);
            player.SetActive(true);
        }
        
        if (gameData.gameState == GameStates.Started)
        {
            if (IsPlayerDed() || isPlayerDead)
            {
                gameData.gameState = GameStates.Ended;
                isPlayerDead = false;
            }
        }
    }

    PowerUpFactory powerUpFactory = new PowerUpFactory();


    public void PlayerEquippedAbility(string id)
    {
        PowerUp powerUp = powerUpFactory.GetPowerUp(id);
        if (powerUp.powerUpAtStart)
            powerUp.UsePowerUp();

        powerUpQueue.Enqueue(powerUp);
    }
}
