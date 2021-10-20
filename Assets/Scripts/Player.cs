using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;
    private GameManager gm;
    private GameManager.GameStates gameState;
    public static event Action PlayerScored, PlayerCollectedCoin, PlayerHit, PlayerDed, PlayerSaved;
    public static event Action<GameObject> PlayerHitSomething; 
    private float playerSlideThreshold;
    [SerializeField]
    private GameObject explosion;
    private float cameraScreenWidth;
    private float[] xPos;
    public GamedataScriptableObject gameData;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private float boundary;

    private void Start()
    {
       
        Vector3 screenWidth = new Vector3(Screen.width, 0, 0);
        playerSlideThreshold = 100;
        cameraScreenWidth = Camera.main.ScreenToWorldPoint(screenWidth).x;
        boundary = (float) Math.Round((cameraScreenWidth - 0.3f), 2);
        gm = GameManager.instance;
        AdsManager.RewardedVideoAdFinished += () =>
            PlayerSaved?.Invoke();
    }


    private void Update()
    {
        if (gameData.gameState == GameManager.GameStates.Started)
            Shift();
    }

    [HideInInspector]
    public float startTouchPos, endTouchPos;
   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Shift()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPos = Input.GetTouch(0).position.x;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position.x;
            float diff = endTouchPos - startTouchPos;
                        
            if (Math.Abs(diff) > playerSlideThreshold)
            {
                if (diff > 0 && transform.position.x < boundary)
                    StartCoroutine(ShiftHelper("Right"));
                if (diff < 0 && transform.position.x > -boundary)
                    StartCoroutine(ShiftHelper("Left"));
            }
        }
    }


    private float flyTime;
    private Vector3 startRocketPos, endRocketPosition;
    [SerializeField]
    private float flightDuration = 0.3f;

    private IEnumerator ShiftHelper(string dir)
    {
        flyTime = 0f;
        startRocketPos = transform.position;

        switch (dir)
        {
            case "Right":
                endRocketPosition = new Vector3(startRocketPos.x + boundary, transform.position.y, transform.position.z);
                break;
            case "Left":
                endRocketPosition = new Vector3(startRocketPos.x -boundary, transform.position.y, transform.position.z);
                break;
        }
        while(flyTime < flightDuration)
        {
            flyTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startRocketPos, endRocketPosition, flyTime / flightDuration);
            yield return null;
        }

    }


    private int lastInstanceId, instanceId;
    private GameObject explosionParticleSystem;
    private void OnTriggerEnter(Collider other)
    {
        instanceId = other.gameObject.GetInstanceID();
        if (instanceId == lastInstanceId)
            return;

        lastInstanceId = instanceId;

        PlayerHitSomething?.Invoke(other.gameObject);

        if (other.CompareTag("Score"))
            PlayerScored?.Invoke();

        if (other.CompareTag("Coin"))
        {
            PlayerCollectedCoin?.Invoke();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Obstacle"))
        {
            PlayerHit?.Invoke();

            if (explosionParticleSystem == null)
                explosionParticleSystem = Instantiate(explosion);
            explosionParticleSystem.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);

            // if dead
            if (gm.IsPlayerDed())
            {
                gameObject.SetActive(false);
                PlayerDed?.Invoke();
            }
        }
    }

}
