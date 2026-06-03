using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishButton : ItemButton<KeyValuePair<FishStats, int>>
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private FishManager fish;

    private int amount;
    private bool sell = false;
    private FishStats fishStats;

    public void SetSell(bool value)
    {
        sell = value;
        priceText.text = fishStats.Price.ToString();
    }

    public override void Setup(KeyValuePair<FishStats, int> data)
    {
        sell = false;
        priceText.text = "";
        fishStats = data.Key;
        image.sprite = data.Key.FishSprite;
        amount = data.Value;
        text.text = data.Value.ToString();
        gameObject.SetActive(true);
    }

    public override void OnButtonClick()
    {
        Inventory.Instance.TryRemoveFish(fishStats);
        amount--;

        if (!sell)
            Bus<PlaceFish>.Raise(new PlaceFish { Fish = fishStats });
        else
            CoinsManager.Instance.AddCoins(fishStats.Price);

        text.text = amount.ToString();
    }

    public override void Hide()
    {
        base.Hide();
    }
}