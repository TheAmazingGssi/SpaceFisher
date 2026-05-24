using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private VisitorPool pool;
    [SerializeField] private VisitorData[] data;
    [SerializeField] private float maxInterval = 60;
    [SerializeField] private float minInterval = 5;
    [SerializeField] private float growthScale = 1000;
    [SerializeField] private float randomRange = 5;

    private int ticketPrice;
    private float currentInterval;

    private void Start()
    {
        Bus<AquariumValueChange>.OnEvent += OnAquariumValueChanged;
        Bus<VisitorLeaving>.OnEvent += OnVisitorLeaving;
    }

    public void Init(int ticketPrice)
    {
        this.ticketPrice = ticketPrice;
        currentInterval = maxInterval;
        StartCoroutine(SpawnLoop());
    }

    private void OnAquariumValueChanged(AquariumValueChange e)
    {
        float t = e.Value / (e.Value + growthScale);
        currentInterval = Mathf.Lerp(maxInterval, minInterval, t);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float interval = currentInterval + Random.Range(-randomRange, randomRange);
            interval = Mathf.Max(0.1f, interval);
            yield return new WaitForSeconds(interval);
            SpawnVisitor();
        }
    }

    private void SpawnVisitor()
    {
        Visitor visitor = pool.Get();
        visitor.transform.position = spawnPoint.position;
        VisitorData selectedData = data[Random.Range(0, data.Length)];
        Vector2 direction = spawnPoint.right;
        visitor.Initialize(selectedData, direction);
        Bus<VisitorSpawned>.Raise(new VisitorSpawned { Visitor = visitor, TicketPrice = ticketPrice });
    }

    private void OnVisitorLeaving(VisitorLeaving e)
    {
        pool.Release(e.Visitor);
    }

    private void OnDestroy()
    {
        Bus<AquariumValueChange>.OnEvent -= OnAquariumValueChanged;
        Bus<VisitorLeaving>.OnEvent -= OnVisitorLeaving;
    }
}