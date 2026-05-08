using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private VisitorPool pool;

    [SerializeField] private VisitorData[] data;

    [ContextMenu("Spawn Visitor")]
    private void SpawnVisitor()
    {
        Visitor visitor = pool.Get();
        visitor.Initialize(data[Random.Range(0, data.Length)]);
        visitor.transform.position = spawnPoint.position;
        Bus<VisitorSpawned>.Raise(new VisitorSpawned { Visitor = visitor });
    }
}
