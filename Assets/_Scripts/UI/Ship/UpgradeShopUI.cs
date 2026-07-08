using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopUI : PanelUI<UpgradeButton, KeyValuePair<Upgrade, int>>
{
    private void Awake()
    {
        Bus<UpgradeBought>.OnEvent += OnUpgradeBought;
    }

    private void OnEnable()
    {
        RefreshPanel(UpgradeManager.Instance.CurrentUpgrades);
    }

    private void OnUpgradeBought(UpgradeBought e)
    {
        RefreshPanel(UpgradeManager.Instance.CurrentUpgrades);
    }

    private void OnDestroy()
    {
        Bus<UpgradeBought>.OnEvent -= OnUpgradeBought;
    }
}