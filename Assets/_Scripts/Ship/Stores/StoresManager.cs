using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoresManager : MonoBehaviour
{
    public static List<BuildingData> AvailableStores { get; private set; } = new List<BuildingData>();
    public static Dictionary<Building, BuildingData> Stores { get; private set; } = new Dictionary<Building, BuildingData>();

    [SerializeField] private List<BuildingData> startingStores = new List<BuildingData>();
    [SerializeField] private StorePool pool;

    private void Start()
    {
        Bus<StoreBought>.OnEvent += OnStoreBought;
        AvailableStores = startingStores;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
#if UNITY_EDITOR
        if (!PlayerPrefs.HasKey(Constants.FirstOpen))
        {
            PlayerPrefs.SetInt(Constants.FirstOpen, 1);
            PlayerPrefs.Save();
        }
        else
        {
            RestoreStores();
        }
#else
        RestoreAquariums();
#endif
    }

    private void OnDestroy()
    {
        Bus<StoreBought>.OnEvent -= OnStoreBought;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name != Constants.Scenes.Ship) return;
        Stores.Clear();
    }

    private void OnStoreBought(StoreBought e)
    {
        Store newStore = pool.Get();
        pool.SetPosition(newStore.gameObject);
        newStore.Init(e.Data);
        Stores[newStore] = e.Data;
    }

    private void RestoreStores()
    {
        foreach (StoreSaveData sd in RunManager.Instance.GetStoreStates())
        {
            BuildingData data = ScriptablesDatabase.Instance.storeList[sd.BuildingDataId];
            Store store = pool.Get();
            store.transform.position = sd.Position;
            store.Init(data);
            Stores[store] = data;
        }
    }
}