using UnityEngine;

[CreateAssetMenu(fileName ="New GameData", menuName ="New GameData")]
public class GamedataScriptableObject : ScriptableObject
{
    //[HideInInspector]
    public GameManager.GameStates gameState = GameManager.GameStates.Default;
    public readonly int playerInitialHealth = 10;
    public int playerHighScore;
    public int playerTotalCoins;
}
