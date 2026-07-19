using TMPro;
using UnityEngine;

public class StoreButton : ItemButton<StoreData>
{
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI priceText;

    private StoreData data;
    public override void Setup(StoreData data)
    {
        this.data = data;
        valueText.text = data.Value[0].ToString();
        priceText.text = data.Price[0].ToString();
        image.sprite = data.Sprites[0];
        text.text = data.name;
    }

    public override void OnButtonClick()
    {
        if (CoinsManager.Instance.TryBuying(data.Price[0]))
            Bus<StoreBought>.Raise(new StoreBought { Data = data });
        else
            Debug.Log("Not enough coins");

    }
}
