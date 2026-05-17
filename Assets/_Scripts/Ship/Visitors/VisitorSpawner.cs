using System.Collections;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private VisitorPool pool;
    [SerializeField] private VisitorData[] data;
    [SerializeField] public int TicketPrice = 10;

    private float _currentInterval = 60f;

    public void SetInterval(float interval)
    {
        _currentInterval = interval;
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_currentInterval);
            SpawnVisitor();
        }
    }

    private void SpawnVisitor()
    {
        Visitor visitor = pool.Get();
        visitor.Initialize(data[Random.Range(0, data.Length)]);
        visitor.transform.position = spawnPoint.position;
        Bus<VisitorSpawned>.Raise(new VisitorSpawned { Visitor = visitor, TicketPrice = TicketPrice });
        Debug.Log("Visitor spawned");
    }
}