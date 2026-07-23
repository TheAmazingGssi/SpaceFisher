using System.Collections.Generic;
using UnityEngine;

public class FishStorePanel : PanelUI<SellFishButton, KeyValuePair<FishStats, int>>
{
    private void OnEnable()
    {
        Bus<FishInventoryChange>.OnEvent += OnInventoryChange;
        RefreshPanel(Inventory.Instance.Fish);
    }


    private void OnDisable()
    {
        Bus<FishInventoryChange>.OnEvent -= OnInventoryChange;
    }

    private void OnInventoryChange(FishInventoryChange e)
    {
        RefreshPanel(Inventory.Instance.Fish);
    }

    [ContextMenu("Refresh Panel")]
    public void RefreshPanel()
    {
        RefreshPanel(Inventory.Instance.Fish);
    }
}
