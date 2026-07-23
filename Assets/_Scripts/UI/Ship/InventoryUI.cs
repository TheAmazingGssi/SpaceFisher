using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : PanelUI<InventoryFishButton, KeyValuePair<FishStats, int>> //TODO: fish button stay when invetory empty
{
    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OnAquariumPressed;
    }

    private void OnAquariumPressed(AquariumPressed e)
    {
        RefreshPanel(Inventory.Instance.Fish);
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= OnAquariumPressed;
    }
}