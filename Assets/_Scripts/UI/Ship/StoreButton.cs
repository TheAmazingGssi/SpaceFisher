using TMPro;
using UnityEngine;

public class StoreButton : ItemButton<BuildingData>
{
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI priceText;

    private BuildingData data;
    public override void Setup(BuildingData data)
    {
        this.data = data;
        valueText.text = data.Value.ToString();
        priceText.text = data.Price.ToString();
        image.sprite = data.Sprite;
    }

    public override void OnButtonClick()
    {
        if (CoinsManager.Instance.TryBuying(data.Price))
            Bus<StoreBought>.Raise(new StoreBought { Data = data });
        else
            Debug.Log("Not enough coins");

    }
}
