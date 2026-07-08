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

    [Serializable]
    private struct UpgradeEntry
    {
        public Upgrade Upgrade;
        public UpgradeType Type;
    }

    [SerializeField] private UpgradeTable table;
    [SerializeField] private UpgradeEntry[] upgradeTypes;

    private readonly Dictionary<Upgrade, UpgradeType> typeLookup = new();
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

        foreach (var entry in upgradeTypes)
            typeLookup[entry.Upgrade] = entry.Type;

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

    public UpgradeType GetUpgradeType(Upgrade upgrade) => typeLookup[upgrade];

    public bool TryUpgrade(Upgrade upgrade)
    {
        UpgradeType type = typeLookup[upgrade];
        int level = CurrentUpgrades[upgrade];

        if (level >= type.MaxLevel)
        {
            Debug.Log("Already at max level");
            return false;
        }

        int cost = Mathf.RoundToInt(type.MoneyCost[level + 1]);
        if (!CoinsManager.Instance.TryBuying(cost))
        {
            Debug.Log("Not enough coins");
            return false;
        }

        CurrentUpgrades[upgrade] = level + 1;
        Bus<UpgradeBought>.Raise(new UpgradeBought { Upgrade = upgrade, NewLevel = level + 1 });
        return true;
    }

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
}