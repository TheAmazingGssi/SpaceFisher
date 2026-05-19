using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StoresManager : MonoBehaviour
{
    public static List<StoreData> Stores { get; private set; } = new List<StoreData>();

    [SerializeField] private List<StoreData> startingStores = new List<StoreData>();
    [SerializeField] private StorePool pool;

    private void Start()
    {
        Bus<StoreBought>.OnEvent += OnStoreBought;
        Stores = startingStores;
    }

    private void OnStoreBought(StoreBought e)
    {
        Store newStore = pool.Get();
        newStore.Init(e.Data);
    }

    private void OnDestroy()
    {
        Bus<StoreBought>.OnEvent -= OnStoreBought;
    }
}
