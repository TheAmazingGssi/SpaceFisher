using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private FishStorePanel fishStoreUI;

    [Header("UI Elements")]
    [SerializeField] private GameObject fishPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI CurrentCoinsText;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject firstAqButton;

    private bool fishStoreOpen = false;


    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OpenFishPanel;
        Bus<PlaceFish>.OnEvent += CloseFishPanel;
        Bus<CoinChange>.OnEvent += OnCoinChange;
        Bus<StoreBought>.OnEvent += _ => ToggleShop();
        Bus<AquariumBought>.OnEvent += _ => ToggleShop();

#if UNITY_EDITOR
        if(RunManager.Instance.gameStart)
            firstAqButton.SetActive(true);
#else
        if (!PlayerPrefs.HasKey(Constants.FirstOpen))
        {
            Inventory.Instance.ClearInventory();
            SaveManager.Instance.Delete();
            firstAqButton.SetActive(true);
            PlayerPrefs.SetInt(Constants.FirstOpen, 1);
            PlayerPrefs.Save();
        }
        else firstAqButton.SetActive(false);
#endif
        fishPanel.SetActive(false);
        CurrentCoinsText.text = CoinsManager.Instance.Coins.ToString();

    }

    public void GetFirstAquarium()
    {
        RunManager.Instance.gameStart = false;
        Bus<AquariumBought>.Raise(new AquariumBought());
        firstAqButton.SetActive(false);
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

    public void ToggleFishStore()
    {
        fishStoreOpen = !fishStoreOpen;
        fishStoreUI.gameObject.SetActive(fishStoreOpen);
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