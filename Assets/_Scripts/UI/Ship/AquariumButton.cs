using TMPro;
using UnityEngine;

public class AquariumButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;

    private int price;

    private void Start()
    {
        Bus<AquariumPriceChange>.OnEvent += UpdatePrice;
    }

    private void UpdatePrice(AquariumPriceChange e)
    {
        price = e.Price;
        priceText.text = price.ToString();
    }

    public void OnButtonClicked()
    {
        Debug.Log("Aquarium pressed");
        if (CoinsManager.Instance.TryBuying(price))
            Bus<AquariumBought>.Raise(new AquariumBought());
    }

    private void OnDestroy()
    {
        Bus<AquariumPriceChange>.OnEvent -= UpdatePrice;
    }
}
