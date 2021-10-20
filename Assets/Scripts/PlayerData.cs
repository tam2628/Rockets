public class PlayerData
{
    private static int playerScore = 0,
    playerHighScore = 0,
    playerTotalNuggets = 0,
    playerNuggets = 0,
    playerHealth = 10,
    playerShield = 0;

    public static void AddPlayerHealth(int healthPoints) => playerHealth += healthPoints;
    public static int GetPlayerHealth() => playerHealth;
    public static void AddPlayerNuggets(int nuggetPoints) => playerNuggets += nuggetPoints;
    public static int GetPlayerNuggets() => playerNuggets;
    public static void AddPlayerScore(int points) => playerScore += points;
    public static int GetPlayerScore() { return playerScore; }
    
    public static void ResetPlayerData()
    {
        playerScore = 0;
        playerNuggets = 0;
        playerHealth = 10;
    }

    public static bool SaveGameData()
    {
        // Some complex things lol
        return true;
    }
}
