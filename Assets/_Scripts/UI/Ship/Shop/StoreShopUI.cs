using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreShopUI : PanelUI<StoreButton, BuildingData>
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button aquariumButton;

    private int price;

    private void Awake()
    {
        Bus<AquariumPriceChange>.OnEvent += UpdatePrice;
        aquariumButton.onClick.AddListener(OnAquariumClicked);
    }

    private void OnEnable()
    {
        RefreshPanel(StoresManager.AvailableStores);
    }

    public override void RefreshPanel(IEnumerable<BuildingData> items)
    {
        base.RefreshPanel(items);
        priceText.text = price.ToString();
    }

    private void UpdatePrice(AquariumPriceChange e)
    {
        price = e.Price;
        priceText.text = price.ToString();
    }

    [ContextMenu("Buy Aquarium")]
    public void OnAquariumClicked()
    {
        if (CoinsManager.Instance.TryBuying(price))
            Bus<AquariumBought>.Raise(new AquariumBought());
    }

    private void OnDestroy()
    {
        Bus<AquariumPriceChange>.OnEvent -= UpdatePrice; 
        aquariumButton.onClick.RemoveAllListeners();

    }
}
