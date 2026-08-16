using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }
    public int Coins { get; private set; }
    private List<Vector3> visitorPos = new List<Vector3>();
    private List<AquariumSaveData> aquariumStates = new List<AquariumSaveData>();
    private List<StoreSaveData> storeStates = new List<StoreSaveData>();

    private float timeAway = -1;
    private float spawnInterval = 60;
    private float storeIncome = 0;
    private int visitorsAway = 0;

    public string ShipName { get; private set; } = "My Ship";
    public bool Present {  get; private set; } = false;
    public bool FirstRun {  get; private set; } = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(SceneManager.GetActiveScene().name == Constants.Scenes.PresentationShip)
            Present = true;
    }

    private void OnEnable()
    {
        Bus<CoinChange>.OnEvent += OnCoinChange;
        SceneManager.activeSceneChanged += FirstSceneChange;
    }

    private void OnDisable()
    {
        Bus<CoinChange>.OnEvent -= OnCoinChange;
    }

    public void UpdateShipName(string name)
    {
        ShipName = name;
    }
    private void FirstSceneChange(Scene a, Scene b)
    {
        if(FirstRun && b.name == Constants.Scenes.Minigame) FirstRun = false;
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
    public int GetVisitorsAway()
    {
        int count = visitorsAway;
        visitorsAway = 0;
        return count;
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
        GameSaveData data = Snapshot();
        storeStates = data.Stores;
        aquariumStates = data.Aquariums;
        visitorPos = data.VisitorPositions;
        print("CacheCurrentScene: " + visitorPos.Count);
    }

    public GameSaveData Snapshot()
    {
        GameSaveData data = new GameSaveData { Coins = Coins, ShipName = this.ShipName };
        foreach (Aquarium aq in AquariumManager.Aquariums)
        {
            AquariumSaveData aqData = new AquariumSaveData
            {
                XPos = aq.transform.position.x,
                YPos = aq.transform.position.y
            };
            foreach (FishStats stats in aq.Fish)
                aqData.FishIds.Add(stats.ID);
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
                Level = kvp.Key.Level
            });
        }
        if(UpgradeManager.Instance.CurrentUpgrades.Count > 0)
        {
            foreach (KeyValuePair<Upgrade, int> kvp in UpgradeManager.Instance.CurrentUpgrades)
            {
                switch (kvp.Key)
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
        }
        foreach (Visitor v in VisitorsManager.Visitors)
        {
            if (v == null) continue;
            data.VisitorPositions.Add(v.transform.position);
        }
       // print("Run manager VisitorsManager.Visitors: " + VisitorsManager.Visitors.Count);
        return data;
    }

    public void RestoreFrom(GameSaveData data)
    {
        Coins = data.Coins;
        storeStates = data.Stores;
        aquariumStates = data.Aquariums;
        visitorPos = data.VisitorPositions;
    }

    public List<AquariumSaveData> GetAquariumStates() => aquariumStates;
    public List<StoreSaveData> GetStoreStates() => storeStates;
    public List<Vector3> GetVisitorPos() => visitorPos;
}