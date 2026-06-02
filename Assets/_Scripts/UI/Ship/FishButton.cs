using System.Collections.Generic;
using UnityEngine;

public class FishButton : ItemButton<KeyValuePair<FishStats, int>>
{
    [SerializeField] private FishManager fish;

    private bool sell = false;
    private FishStats fishStats;

    public void SetSell(bool value)
    {
        sell = value;
    }

    public override void Setup(KeyValuePair<FishStats, int> data)
    {
        fishStats = data.Key;
        image.sprite = data.Key.FishSprite;
        image.preserveAspect = true;
        text.text = data.Value.ToString();
        gameObject.SetActive(true);
    }

    public override void OnButtonClick()
    {
        if (!sell)
        {
            Inventory.Instance.TryRemoveFish(fishStats);
            Bus<PlaceFish>.Raise(new PlaceFish { Fish = fishStats });
        }
        else
        {
            CoinsManager.Instance.AddCoins(fishStats.Price);
        }
    }

    public override void Hide()
    {
        sell = false;
        base.Hide();
    }
}