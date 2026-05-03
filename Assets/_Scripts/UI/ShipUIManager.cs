using UnityEngine;

public class ShipUIManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPanel;


    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += OpenFishPanel;
        Bus<PlaceFish>.OnEvent += CloseFishPanel;
    }

    private void OpenFishPanel(AquariumPressed e)
    {
        fishPanel.SetActive(true);
    }

    private void CloseFishPanel(PlaceFish e)
    {
        fishPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= OpenFishPanel;
        Bus<PlaceFish>.OnEvent -= CloseFishPanel;
    }
}
