[System.Serializable]
public class Reward
{
    public int coins;
    public float exp;
    public Item item;

    public override string ToString()
    {
        return "Coins: " + this.coins + "\n" +
            "Exp: " + this.exp;
    }
}
