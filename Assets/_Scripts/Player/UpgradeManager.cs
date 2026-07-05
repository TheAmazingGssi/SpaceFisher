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
        Bus<LoadData>.OnEvent += LoadUpgrades;
    }

    private void OnDestroy()
    {
        Bus<LoadData>.OnEvent -= LoadUpgrades;
    }

    private void Start()
    {
    }

    public int GetUpgrade(Upgrade upgrade) => CurrentUpgrades[upgrade];

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
