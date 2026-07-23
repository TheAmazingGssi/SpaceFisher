using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellFishButton : FishButton
{
    [SerializeField] private TextMeshProUGUI priceText;

    public override void Setup(KeyValuePair<FishStats, int> data)
    {
        base.Setup(data);
        priceText.text = fishStats.Price.ToString();
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        CoinsManager.Instance.AddCoins(fishStats.Price);
    }
}