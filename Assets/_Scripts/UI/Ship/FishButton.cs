using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishButton : ItemButton<KeyValuePair<FishStats, int>>
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private FishManager fish;

    private bool sell = false;
    private FishStats fishStats;

    public void SetSell(bool value)
    {
        sell = value;
        priceText.text = fishStats.Price.ToString();
    }

public override void Setup(KeyValuePair<FishStats, int> data)
{
    fishStats = data.Key;
    image.sprite = data.Key.FishSprite;
    text.text = data.Value.ToString();
    gameObject.SetActive(true);
}

    public override void OnButtonClick()
    {
        Inventory.Instance.TryRemoveFish(fishStats);

        if (!sell)
            Bus<PlaceFish>.Raise(new PlaceFish { Fish = fishStats });
        else
            CoinsManager.Instance.AddCoins(fishStats.Price);
    }

    public override void Hide()
    {
        sell = false;
        base.Hide();
    }
}