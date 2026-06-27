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
    [SerializeField] private UIController uiController;

    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OpenFishPanel;
        Bus<PlaceFish>.OnEvent += CloseFishPanel;
        Bus<CoinChange>.OnEvent += OnCoinChange;
        Bus<StoreBought>.OnEvent += _ => ToggleShop();
        Bus<AquariumBought>.OnEvent += _ => ToggleShop();

        fishPanel.SetActive(false);
        CurrentCoinsText.text = CoinsManager.Instance.Coins.ToString();
    }

    public void GoToMiniGame()
    {
        RunManager.Instance.CacheCurrentScene();
        SceneManager.LoadScene(Constants.Scenes.Minigame);
    }

    [ContextMenu("Open Shop Window")]
    public void ToggleShop()
    {
        uiController.SetState(false);
    }

    public void CloseStorePanel()
    {
        uiController.SetState(false);
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
        Bus<StoreBought>.OnEvent -= _ => ToggleShop();
        Bus<AquariumBought>.OnEvent -= _ => ToggleShop();
    }
}