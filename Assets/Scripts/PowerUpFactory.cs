public class PowerUpFactory
{
    public PowerUp GetPowerUp(string id)
    {
        switch(id){
            case "doublecoin":
                return new DoubleCoinPowerUp() as PowerUp;
            case "shield":
                return new ShieldPowerUp() as PowerUp;
        }

        return null;
    }
}
