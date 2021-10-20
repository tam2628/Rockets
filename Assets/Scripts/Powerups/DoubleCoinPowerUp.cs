using UnityEngine;

[CreateAssetMenu(fileName = "NewDoubleCoinPowerUp", menuName = "Shop/Powerups/NewDoubleCoinPowerUp")]
public class DoubleCoinPowerUp : PowerUp
{
    public override void UsePowerUp()
    {
        Debug.Log("Used double coin power up");
        GameManager.instance.playerCoins *= 2;
    }
}
