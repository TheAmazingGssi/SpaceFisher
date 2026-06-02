using System.Collections.Generic;
using UnityEngine;

public class FishStorePanel : PanelUI<FishButton, KeyValuePair<FishStats, int>>
{
    private void OnEnable()
    {
        RefreshPanel(Inventory.Instance.Fish);
    }

    protected override void OnButtonSetup(FishButton button)
    {
        button.SetSell(true);
    }
}
