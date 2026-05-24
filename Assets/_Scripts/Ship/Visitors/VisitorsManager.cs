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
        Bus<VisitorReleased>.OnEvent += OnVisitorReleased;
        Bus<VisitorLeaving>.OnEvent += OnVisitorLeaving;
        spawner.Init(ticketPrice);
    }

    private void OnVisitorSpawned(VisitorSpawned e)
    {
        Visitors.Add(e.Visitor);
        CoinsManager.Instance.AddCoins(e.TicketPrice);
    }

    private void OnVisitorReleased(VisitorReleased e)
    {
        if (e.Building.BuildingType == Location.Aquarium)
        {
            e.Visitor.ResumeMovement();
        }
        else
        {
            e.Visitor.gameObject.SetActive(true);
            e.Visitor.ResumeMovement();
        }
    }

    private void OnVisitorLeaving(VisitorLeaving e)
    {
        Visitors.Remove(e.Visitor);
    }

    private void OnDestroy()
    {
        Bus<VisitorSpawned>.OnEvent -= OnVisitorSpawned;
        Bus<VisitorReleased>.OnEvent -= OnVisitorReleased;
        Bus<VisitorLeaving>.OnEvent -= OnVisitorLeaving;
    }
}