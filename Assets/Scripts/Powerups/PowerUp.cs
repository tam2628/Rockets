public abstract class PowerUp: ShopItem, IPowerup
{
    public bool powerUpAtStart;
    public bool autoEquipable = true;
    public abstract void UsePowerUp();

    public PowerUp(bool powerUpAtStart = false) => this.powerUpAtStart = powerUpAtStart;
}