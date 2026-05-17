using System.Collections.Generic;
using UnityEngine;

public class VisitorsManager : MonoBehaviour
{
    [SerializeField] private VisitorSpawner spawner;
    [SerializeField] private AquariumManager aquariumManager;
    [SerializeField] private ZoneBase[] zones;

    [Header("Spawn Timing")]
    [SerializeField] private float maxInterval = 60f;
    [SerializeField] private float minInterval = 5f;
    [SerializeField] private float valueCap = 1000f;

    public static List<Visitor> Visitors = new List<Visitor>();

    private void Awake()
    {
        Bus<OfflineTimeCalculated>.OnEvent += OnOfflineTimeCalculated;
    }
    private void Start()
    {
        Bus<VisitorSpawned>.OnEvent += OnVisitorSpawned;
        Bus<AquariumValueChange>.OnEvent += OnAquariumValueChanged;

        float initialInterval = CalculateInterval(aquariumManager.CalculateValue());
        spawner.SetInterval(initialInterval);
    }

    private void OnOfflineTimeCalculated(OfflineTimeCalculated e)
    {
        float interval = CalculateInterval(aquariumManager.CalculateValue());
        int visitorsSpawned = Mathf.FloorToInt(e.SecondsOffline / interval);
        CoinsManager.Instance.AddCoins(visitorsSpawned * spawner.TicketPrice);
    }

    private void OnAquariumValueChanged(AquariumValueChange e)
    {
        spawner.SetInterval(CalculateInterval(e.Value));
    }

    private float CalculateInterval(float totalValue)
    {
        float t = Mathf.Clamp01(totalValue / valueCap);
        return Mathf.Lerp(maxInterval, minInterval, t);
    }

    private void OnVisitorSpawned(VisitorSpawned e)
    {
        Visitors.Add(e.Visitor);
        CoinsManager.Instance.AddCoins(e.TicketPrice);
    }

    private void OnDestroy()
    {
        Bus<VisitorSpawned>.OnEvent -= OnVisitorSpawned;
        Bus<AquariumValueChange>.OnEvent -= OnAquariumValueChanged;
        Bus<OfflineTimeCalculated>.OnEvent -= OnOfflineTimeCalculated;
    }
}