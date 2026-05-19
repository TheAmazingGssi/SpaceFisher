using System.Collections.Generic;
using UnityEngine;

public class FishButton : ItemButton<KeyValuePair<FishStats, int>>
{
    [SerializeField] private FishManager fish;

    public override void Setup(KeyValuePair<FishStats, int> data)
    {
        fishStats = data.Key;
        image.sprite = data.Key.FishSprite;
        text.text = data.Value.ToString();
        gameObject.SetActive(true);
    }

    private FishStats fishStats;

    public override void OnButtonClick()
    {
        fish.Initialize(fishStats);
        Inventory.Instance.TryRemoveFish(fishStats);
        Bus<PlaceFish>.Raise(new PlaceFish { Fish = fish });
    }
}