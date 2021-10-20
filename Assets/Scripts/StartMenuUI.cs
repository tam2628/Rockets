using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    public static event Action PlayButtonPressed, SettingsButtonPressed, ShopButtonPressed;
    [SerializeField]
    private TextMeshProUGUI highScoreText, playerCoinsText;
    [SerializeField]
    private GamedataScriptableObject gamedata;

    public void PlayButtonPressedAction() => PlayButtonPressed?.Invoke();
    public void SettingsButtonPressedAction() => SettingsButtonPressed?.Invoke();
    public void ShopButtonPressedAction() => SceneManager.LoadScene(2);

    private void Start()
    {
        Setdata();
    }

    private void Setdata()
    {
        highScoreText.text = gamedata.playerHighScore.ToString();
        playerCoinsText.text = gamedata.playerTotalCoins.ToString();
    }  
}
