using System;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade
{
    Magnet,
    Shield,
    Length
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private UpgradeTable table;
    [SerializeField] private UpgradeType[] upgradeTypes;

    private readonly UpgradeType[] typeByUpgrade = new UpgradeType[Enum.GetValues(typeof(Upgrade)).Length];

    public Dictionary<Upgrade, int> CurrentUpgrades { get; private set; } = new Dictionary<Upgrade, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var type in upgradeTypes)
            typeByUpgrade[(int)type.Upgrade] = type;

        foreach (Upgrade upgrade in Enum.GetValues(typeof(Upgrade)))
            if (!CurrentUpgrades.ContainsKey(upgrade))
                CurrentUpgrades[upgrade] = 0;

        Bus<LoadData>.OnEvent += LoadUpgrades;
        Bus<UpgradeBought>.OnEvent += OnUpgradeBought;
    }

    private void OnDestroy()
    {
        Bus<LoadData>.OnEvent -= LoadUpgrades;
        Bus<UpgradeBought>.OnEvent -= OnUpgradeBought;
    }

    public int GetUpgrade(Upgrade upgrade) => CurrentUpgrades[upgrade];
    public UpgradeType GetUpgradeType(Upgrade upgrade) => typeByUpgrade[(int)upgrade];

    private void OnUpgradeBought(UpgradeBought e)
    {
        CurrentUpgrades[e.Upgrade] = e.NewLevel;
    }

    private void LoadUpgrades(LoadData e)
    {
        foreach (KeyValuePair<Upgrade, int> kvp in CurrentUpgrades)
        {
            switch (kvp.Key)
            {
                case Upgrade.Magnet:
                    e.Data.Upgrades.Magnet = kvp.Value;
                    break;
                case Upgrade.Shield:
                    e.Data.Upgrades.Shield = kvp.Value;
                    break;
                case Upgrade.Length:
                    e.Data.Upgrades.Length = kvp.Value;
                    break;
            }
        }
    }

    [ContextMenu("Print upgrades")]
    public void PrintUpgrades()
    {
        foreach (KeyValuePair<Upgrade, int> kvp in CurrentUpgrades)
            print($"Type: {kvp.Key}  Level: {kvp.Value}");
    }
}