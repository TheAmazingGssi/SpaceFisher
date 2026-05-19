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
    [SerializeField] private TextMeshProUGUI CurrentCoinsText;


    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OpenFishPanel;
        Bus<PlaceFish>.OnEvent += CloseFishPanel;
        Bus<CoinChange>.OnEvent += OnCoinChange;
        Bus<StoreBought>.OnEvent += CloseStorePanel;
    }

    public void GoToMiniGame()
    {
        SceneManager.LoadScene(Constants.Scenes.Minigame);
    }

    [ContextMenu("Open Shop Window")]
    public void OpenStorePanel()
    {
        storeShopUI.gameObject.SetActive(true);
        storeShopUI.RefreshPanel(StoresManager.Stores);
    }

    private void CloseStorePanel(StoreBought e)
    {
        storeShopUI.gameObject.SetActive(false);
    }

    private void OpenFishPanel(AquariumPressed e)
    {
        fishPanel.SetActive(true);
    }

    private void CloseFishPanel(PlaceFish e)
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
    }
}
