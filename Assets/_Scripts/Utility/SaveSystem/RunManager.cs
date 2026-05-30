using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }

    public int Coins { get; private set; }

    private List<AquariumSaveData> aquariumStates = new List<AquariumSaveData>();
    private List<StoreSaveData> storeStates = new List<StoreSaveData>();

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

        foreach (KeyValuePair<Building, BuildingData> kvp in StoresManager.Stores)
        {
            if (kvp.Key == null) continue;
            data.Stores.Add(new StoreSaveData
            {
                BuildingDataId = kvp.Value.ID,
                XPos = kvp.Key.transform.position.x,
                YPos = kvp.Key.transform.position.y
            });
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