using UnityEngine;

[CreateAssetMenu(fileName = "NewShieldPowerup", menuName = "Shop/Powerups/NewShieldPowerup")]
public class ShieldPowerUp : PowerUp
{
    public ShieldPowerUp() : base(true) { }

    private int shieldCapacity = 10;
    public override void UsePowerUp() {
        GameManager.instance.IncreasePlayerHealth(shieldCapacity);
    }
}
