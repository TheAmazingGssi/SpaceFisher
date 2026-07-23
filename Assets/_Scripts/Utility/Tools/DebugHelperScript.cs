using System.Collections.Generic;
using UnityEngine;

public class DebugHelperScript : MonoBehaviour
{
    [SerializeField] private FishStats randomFishy;
    [SerializeField] private FishStats randomFishy2;
    [SerializeField] private FishStats randomFishy3;
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
    [ContextMenu("fill inventory")]
    public void FillInventory()
    {
       Inventory.Instance.AddFish(randomFishy, 25);
       Inventory.Instance.AddFish(randomFishy2, 5);
       Inventory.Instance.AddFish(randomFishy3, 1);
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
