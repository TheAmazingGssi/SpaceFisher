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


    public bool TryBuying(int cost)
    {
        if(cost <= Coins)
        {
            Coins -= cost;
            return true;
        }
        return false;
    }
}
