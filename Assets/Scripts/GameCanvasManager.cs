using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText, coinText;
    private GameManager gm;
    private bool playerHitSomething = false;
    [SerializeField]
    private GameObject countDownText;
    [SerializeField]
    private float countDownDuartionForEachCount = 0.3f;
    [SerializeField]
    private GameObject coinImageGameObject;
    [SerializeField]
    private GameObject equipAbilityMenu;
    public static event Action<string> GameSceneLoaded;
        
    private IEnumerator Countdown()
    {
        scoreText.gameObject.SetActive(false);
        coinText.gameObject.SetActive(false);
        coinImageGameObject.SetActive(false);
        countDownText.SetActive(true);
        RectTransform rt = countDownText.GetComponent<RectTransform>();
        TextMeshProUGUI text_TMPro = countDownText.GetComponent<TextMeshProUGUI>();
        rt.localScale = Vector3.zero;
        for(int i = 3; i > 0; --i)
        {
            text_TMPro.text = i.ToString();
            LeanTween.scale(rt, Vector3.one, countDownDuartionForEachCount);
            rt.localScale = Vector3.zero;
            yield return new WaitForSeconds(countDownDuartionForEachCount);
        }

        text_TMPro.text = "Play!";
        LeanTween.scale(rt, Vector3.one, countDownDuartionForEachCount);
        rt.localScale = Vector3.zero;
        yield return new WaitForSeconds(countDownDuartionForEachCount);
        countDownText.SetActive(false);
        scoreText.gameObject.SetActive(true);
        coinText.gameObject.SetActive(true);
        coinImageGameObject.SetActive(true);
        gm.GameScreenCountdownOver();
    }

    private void Awake()
    {
        gm = GameManager.instance;

        scoreText.text = gm.playerScore.ToString();
        coinText.text = gm.playerCoins.ToString();

        Player.PlayerHitSomething += (GameObject _) => playerHitSomething = true;
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            GameSceneLoaded?.Invoke(scene.name);
            //EquipAbilitiesPanelScript.instance.OpenMenu();
            //if (scene.name == "GameScene" && this != null)
            //StartCoroutine(Countdown());
        };
        GameManager.GameResumed += () =>
        {
            if (this != null)
                StartCoroutine(Countdown());
        };
    }

    private void Start()
    {
        EquipAbilitiesPanelScript.EquipAbilitiesMenuClosed += () =>
        {
            if (this != null)
                StartCoroutine(Countdown());
        };

        scoreText.gameObject.SetActive(false);
        coinText.gameObject.SetActive(false);
        coinImageGameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerHitSomething)
        {
            scoreText.text = gm.playerScore.ToString();
            coinText.text = gm.playerCoins.ToString();
            playerHitSomething = false;
        }
    }

}
