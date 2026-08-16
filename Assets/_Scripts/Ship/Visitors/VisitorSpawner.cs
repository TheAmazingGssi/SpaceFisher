using System.Collections;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform coinSpawnPoint;
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

        #if UNITY_EDITOR
        if (!PlayerPrefs.HasKey(Constants.FirstOpen))
        {
            PlayerPrefs.SetInt(Constants.FirstOpen, 1);
            PlayerPrefs.Save();
        }
        else
        {
            RestoreVisitors();
        }
#else
        RestoreVisitors();
#endif
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
        RunManager.Instance.SaveSpawnInterval(currentInterval);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float interval = currentInterval + Random.Range(-randomRange, randomRange);
            interval = Mathf.Max(0.1f, interval);
            yield return new WaitForSeconds(interval);
            SpawnVisitor(spawnPoint.position);
        }
    }

    private void SpawnVisitor(Vector3 pos)
    {
        Visitor visitor = pool.Get();
        visitor.transform.position = pos;
        VisitorData selectedData = data[Random.Range(0, data.Length)];
        Vector2 direction = spawnPoint.right;
        visitor.Initialize(selectedData, direction);
        Bus<VisitorSpawned>.Raise(new VisitorSpawned { Visitor = visitor, TicketPrice = ticketPrice });
    }

    private void RestoreVisitors()
    {
        foreach (Vector3 pos in RunManager.Instance.GetVisitorPos())
        {
            SpawnVisitor(pos);
        }
       // print("spawner GetVisitorPos(): " + RunManager.Instance.GetVisitorPos().Count);
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