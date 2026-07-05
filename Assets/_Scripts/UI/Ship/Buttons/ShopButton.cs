using UnityEngine;
using UnityEngine.UI;
public enum ShopButtons
{
    FishShop,
    BuildingShop,
    UpgradeShop
}
public class ShopButton : MonoBehaviour
{
    [field: SerializeField] public Button Button {  get; private set; }
    [field: SerializeField] public ShopButtons Type {  get; private set; }

    [field: SerializeField] public GameObject Panel { get; private set; }

    private void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        Button.interactable = false;
        Bus<ShopButtonPressed>.Raise(new ShopButtonPressed { ShopButton = this });
    }
}
