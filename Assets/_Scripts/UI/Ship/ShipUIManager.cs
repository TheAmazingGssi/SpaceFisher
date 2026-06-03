using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private StoreShopUI storeShopUI;

    [Header("UI Elements")]
    [SerializeField] private GameObject fishPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI CurrentCoinsText;

    private bool shopToggle = false;


    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OpenFishPanel;
        Bus<PlaceFish>.OnEvent += CloseFishPanel;
        Bus<CoinChange>.OnEvent += OnCoinChange;
        Bus<StoreBought>.OnEvent += CloseStorePanel;
        Bus<AquariumBought>.OnEvent += _ => ToggleShop();

        shopPanel.SetActive(false);
        fishPanel.SetActive(false);
    }

    public void GoToMiniGame()
    {
        RunManager.Instance.CacheCurrentScene();
        SceneManager.LoadScene(Constants.Scenes.Minigame);
    }

    [ContextMenu("Open Shop Window")]
    public void ToggleShop()
    {
        shopToggle = !shopToggle;
        shopPanel.SetActive(shopToggle);
    }

    private void CloseStorePanel(StoreBought e)
    {
        shopPanel.SetActive(false);
    }

    public void CloseStorePanel()
    {
        shopPanel.SetActive(false);
    }

    private void OpenFishPanel(AquariumPressed e)
    {
        fishPanel.SetActive(true);
        inventoryUI.RefreshPanel(Inventory.Instance.Fish);
    }

    private void CloseFishPanel(PlaceFish e)
    {
        fishPanel.SetActive(false);
    }
    public void CloseFishPanel()
    {
        fishPanel.SetActive(false);
    }

    private void OnCoinChange(CoinChange e)
    {
        CurrentCoinsText.text = e.NewCoins.ToString();
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= OpenFishPanel;
        Bus<PlaceFish>.OnEvent -= CloseFishPanel;
        Bus<CoinChange>.OnEvent -= OnCoinChange;
        Bus<StoreBought>.OnEvent -= CloseStorePanel;
        Bus<AquariumBought>.OnEvent -= _ => ToggleShop();
    }
}
