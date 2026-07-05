using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }
    public int Coins { get; private set; }
    private List<AquariumSaveData> aquariumStates = new List<AquariumSaveData>();
    private List<StoreSaveData> storeStates = new List<StoreSaveData>();

    private float timeAway = -1;
    private float spawnInterval = 60;
    private float storeIncome = 0;

    public bool gameStart = true; //TODO: :')

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

    private void OnEnable()
    {
        Bus<CoinChange>.OnEvent += OnCoinChange;
    }

    private void OnDisable()
    {
        Bus<CoinChange>.OnEvent -= OnCoinChange;
    }

    private void OnCoinChange(CoinChange e) => Coins = e.NewCoins;

    public void SaveSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void OnShipSceneUnloading(float enterBuildingChance)
    {
        timeAway = Time.time;

        storeIncome = 0;
        foreach (var kvp in StoresManager.Stores)
            storeIncome += kvp.Key.CurrentValue * enterBuildingChance;
    }

    public int CalculateOfflineEarnings(int ticketPrice)
    {
        if (timeAway < 0) return 0;

        float elapsed = Time.time - timeAway;
        timeAway = -1;

        int visitorsSpawned = Mathf.FloorToInt(elapsed / spawnInterval);
        int earnings = visitorsSpawned * (ticketPrice + Mathf.RoundToInt(storeIncome));
        return earnings;
    }

    public void CacheCurrentScene()
    {
        GameSaveData snapshot = Snapshot();
        storeStates = snapshot.Stores;
        aquariumStates = snapshot.Aquariums;
    }

    public GameSaveData Snapshot()
    {
        GameSaveData data = new GameSaveData { Coins = Coins };
        foreach (Aquarium aq in AquariumManager.Aquariums)
        {
            AquariumSaveData aqData = new AquariumSaveData
            {
                XPos = aq.transform.position.x,
                YPos = aq.transform.position.y
            };
            foreach (FishManager fm in aq.Fish)
                aqData.FishIds.Add(fm.Stats.ID);
            data.Aquariums.Add(aqData);
        }
        foreach (KeyValuePair<StoreBase, StoreData> kvp in StoresManager.Stores)
        {
            if (kvp.Key == null) continue;
            data.Stores.Add(new StoreSaveData
            {
                StoreDataId = kvp.Value.ID,
                XPos = kvp.Key.transform.position.x,
                YPos = kvp.Key.transform.position.y,
                Level = kvp.Value.Level
            });
        }
        foreach(KeyValuePair < Upgrade, int > kvp in UpgradeManager.Instance.CurrentUpgrades)
        {
            switch(kvp.Key)
            {
                case Upgrade.Magnet:
                    data.Upgrades.Magnet = kvp.Value;
                    break;
                case Upgrade.Shield:
                    data.Upgrades.Shield = kvp.Value;
                    break;
                case Upgrade.Length:
                    data.Upgrades.Length = kvp.Value;
                    break;
            }
        }
        return data;
    }

    public void RestoreFrom(GameSaveData data)
    {
        Coins = data.Coins;
        storeStates = data.Stores;
        aquariumStates = data.Aquariums;
    }

    public List<AquariumSaveData> GetAquariumStates() => aquariumStates;
    public List<StoreSaveData> GetStoreStates() => storeStates;
}