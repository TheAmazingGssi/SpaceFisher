using System.Collections.Generic;
using UnityEngine;

public class DebugHelperScript : MonoBehaviour
{
    [SerializeField] private FishStats randomFishy;
    [SerializeField] private int amount;

    [SerializeField] private StorePool pool;
    [SerializeField] private StoreData data;

    [SerializeField] private Aquarium aquarium;

    [ContextMenu("Add fishy to inventory")]
    public void AddFishyToInventory()
    {
        if (amount == 1)
            Inventory.Instance.AddFish(randomFishy);
        else
            Inventory.Instance.AddFish(randomFishy, amount);
    }

    [ContextMenu("Add store")]
    public void AddStore()
    {
        Store store = pool.Get();
        store.Init(data);
        store.transform.position = transform.position;
    }

    [ContextMenu("Open inventory")]
    public void OpenInventory()
    {
        Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = aquarium });
    }

    [ContextMenu("Is in inventory?")]
    public void IsInInventory()
    {
        if(Inventory.Instance.IsInInventory(randomFishy))
        {
            foreach (KeyValuePair<FishStats, int> kvp in Inventory.Instance.Fish)
                if (kvp.Key == randomFishy)
                    Debug.Log("Yes! amount: " + kvp.Value);

        }
        else
        {
            Debug.Log("Not In Inventory");
        }
    }



}
