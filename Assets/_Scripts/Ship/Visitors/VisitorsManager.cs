using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisitorsManager : MonoBehaviour
{
    [SerializeField] private AquariumManager aquariumManager;
    [SerializeField] private VisitorSpawner spawner;
    [SerializeField] private int ticketPrice = 10;
    [SerializeField] private VisitorData[] visitorData;

    public static List<Visitor> Visitors = new List<Visitor>();

    private void Start()
    {
        Bus<VisitorSpawned>.OnEvent += OnVisitorSpawned;
        Bus<VisitorReleased>.OnEvent += OnVisitorReleased;
        Bus<VisitorLeaving>.OnEvent += OnVisitorLeaving;
        spawner.Init(ticketPrice);

        int earnings = RunManager.Instance.CalculateOfflineEarnings(ticketPrice);
        if (earnings > 0)
            CoinsManager.Instance.AddCoins(earnings);
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

    private float GetAverageEnterChance()
    {
        if (visitorData == null || visitorData.Length == 0) return 0f;
        float total = 0f;
        foreach (var d in visitorData) total += d.EnterBuildingChance;
        return total / visitorData.Length;
    }


    private void OnDestroy()
    {
        Bus<VisitorSpawned>.OnEvent -= OnVisitorSpawned;
        Bus<VisitorReleased>.OnEvent -= OnVisitorReleased;
        Bus<VisitorLeaving>.OnEvent -= OnVisitorLeaving;

        RunManager.Instance.OnShipSceneUnloading(GetAverageEnterChance());
    }
}