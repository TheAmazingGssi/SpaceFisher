using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipUIManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPanel;
    [SerializeField] private TextMeshProUGUI CurrentCoinsText;


    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OpenFishPanel;
        Bus<PlaceFish>.OnEvent += CloseFishPanel;
        Bus<CoinChange>.OnEvent += OnCoinChange;
    }

    public void GoToMiniGame()
    {
        SceneManager.LoadScene(Constants.Scenes.Minigame);
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
    }
}
