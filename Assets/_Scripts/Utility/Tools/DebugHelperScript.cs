using UnityEngine;

public class DebugHelperScript : MonoBehaviour
{
    [SerializeField] private FishStats randomFishy;
    [SerializeField] private int amount;

    [SerializeField] private StorePool pool;
    [SerializeField] private BuildingData data;

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



}
