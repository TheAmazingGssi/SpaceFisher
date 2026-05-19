using System.Collections.Generic;
using UnityEngine;

public class VisitorsManager : MonoBehaviour
{
    [SerializeField] private AquariumManager aquariumManager;
    [SerializeField] private VisitorSpawner spawner;
    [SerializeField] private int ticketPrice = 10;

    public static List<Visitor> Visitors = new List<Visitor>();

    private void Start()
    {
        Bus<VisitorSpawned>.OnEvent += OnVisitorSpawned;
        spawner.Init(ticketPrice);
    }

    private void OnVisitorSpawned(VisitorSpawned e)
    {
        Visitors.Add(e.Visitor);
        CoinsManager.Instance.AddCoins(e.TicketPrice);
    }

    private void OnDestroy()
    {
        Bus<VisitorSpawned>.OnEvent -= OnVisitorSpawned;
    }
}