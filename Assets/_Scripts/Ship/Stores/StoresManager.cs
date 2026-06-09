using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoresManager : MonoBehaviour
{
    public static List<StoreData> AvailableStores { get; private set; } = new List<StoreData>();
    public static Dictionary<StoreBase, StoreData> Stores { get; private set; } = new Dictionary<StoreBase, StoreData>();

    [SerializeField] private List<StoreData> startingStores = new List<StoreData>();
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
        RestoreStores();
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
        Stores.Add(newStore, e.Data);
    }

    private void RestoreStores()
    {
        foreach (StoreSaveData sd in RunManager.Instance.GetStoreStates())
        {
            StoreData data = ScriptablesDatabase.Instance.storeList[sd.StoreDataId];
            Store store = pool.Get();
            store.transform.position = sd.Position;
            store.Init(data);
            Stores[store] = data;
        }
    }
}