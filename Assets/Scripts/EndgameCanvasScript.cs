using UnityEngine;
using TMPro;
using System;

public class EndgameCanvasScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private GameManager gm;

    private RectTransform gameEndPanel;
    private Vector3 initialScale, finalScale;
    [SerializeField]
    private float duration = 0.5f;
    private AdsManager adsManager;

    public static event Action GotoMainMenuButtonPressed, ShowRewardedAdButtonPressed;
   
    private bool adsWatchedOnce = false;
    [SerializeField]
    private GameObject watchAdButton;

    public static EndgameCanvasScript instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        adsManager = AdsManager.instance;
        gm = GameManager.instance;
        initialScale = new Vector3(0, 0, 0);
        finalScale = new Vector3(1, 1, 1);
        gameEndPanel = transform.GetChild(0).GetComponent<RectTransform>();
        gameEndPanel.localScale = initialScale;
        gameEndPanel.gameObject.SetActive(false);
    }

    public void ShowRewardedAds()
    {
        ShowRewardedAdButtonPressed?.Invoke();
        adsWatchedOnce = true;
        adsManager.ShowRewardAd();
    }

    public void ShowEndGameScreen()
    {
        gameEndPanel.gameObject.SetActive(true);
        scoreText.text = gm.playerScore.ToString();
        LeanTween.scale(gameEndPanel, finalScale, duration);
    }

    public void HideEndGameScreen() { 
        LeanTween.scale(gameEndPanel, initialScale, duration);
        watchAdButton.SetActive(false);
    }
    
    public void GotoMainMenu() => GotoMainMenuButtonPressed?.Invoke();
   
}
