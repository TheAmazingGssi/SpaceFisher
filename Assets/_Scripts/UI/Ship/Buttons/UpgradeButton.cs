using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButton : ItemButton<KeyValuePair<Upgrade, int>>
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI priceText;
    private Upgrade upgrade;
    private int level;

    public override void Setup(KeyValuePair<Upgrade, int> data)
    {
        upgrade = data.Key;
        level = data.Value;

        UpgradeType type = UpgradeManager.Instance.GetUpgradeType(upgrade);
        bool isMaxLevel = level >= type.MaxLevel;

        levelText.text = $"Lv. {level}";
        priceText.text = isMaxLevel ? "MAX" : type.MoneyCost[level + 1].ToString();
        image.sprite = type.Sprite;
    }

    public override void OnButtonClick()
    {
        UpgradeManager.Instance.TryUpgrade(upgrade);
    }
}