using UnityEngine;

public class DebugHelperScript : MonoBehaviour
{
    [SerializeField] private FishStats randomFishy;
    [SerializeField] private int amount;


    [ContextMenu("Add fishy to inventory")]
    public void AddFishyToInventory()
    {
        if (amount == 1)
            Inventory.Instance.AddFish(randomFishy);
        else
            Inventory.Instance.AddFish(randomFishy, amount);
    }
}
