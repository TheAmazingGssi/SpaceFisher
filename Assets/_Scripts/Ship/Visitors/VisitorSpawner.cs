using System.Collections;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private VisitorPool pool;
    [SerializeField] private VisitorData[] data;
    [SerializeField] private float maxInterval = 60;
    [SerializeField] private float minInterval = 5;
    [SerializeField] private float growthScale = 1000;

    private int ticketPrice;
    private float currentInterval;

    private void Start()
    {
        Bus<AquariumValueChange>.OnEvent += OnAquariumValueChanged;
    }

    public void Init(int ticketPrice)
    {
        this.ticketPrice = ticketPrice;
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
            yield return new WaitForSeconds(currentInterval);
            SpawnVisitor();
        }
    }

    private void SpawnVisitor()
    {
        Visitor visitor = pool.Get();
        visitor.Initialize(data[Random.Range(0, data.Length)]);
        visitor.transform.position = spawnPoint.position;
        Bus<VisitorSpawned>.Raise(new VisitorSpawned { Visitor = visitor, TicketPrice = ticketPrice });
    }

    private void OnDestroy()
    {
        Bus<AquariumValueChange>.OnEvent -= OnAquariumValueChanged;
    }
}