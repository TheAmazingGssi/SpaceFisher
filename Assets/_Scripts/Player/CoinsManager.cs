using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;

    public int Coins {  get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        Bus<CoinChange>.Raise( new CoinChange { NewCoins = Coins });
    }

    public bool TryBuying(int cost)
    {
        if(cost <= Coins)
        {
            Coins -= cost;
            Bus<CoinChange>.Raise(new CoinChange { NewCoins = Coins });
            return true;
        }
        return false;
    }
}
