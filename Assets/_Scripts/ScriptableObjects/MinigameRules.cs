using UnityEngine;

[CreateAssetMenu(fileName = "MinigameRules", menuName = "Scriptable Objects/MinigameRules")]
public class MinigameRules : ScriptableObject
{
    [field: SerializeField] public float SpawnTimeMin { get; private set; }
    [field: SerializeField] public float SpawnTimeMax { get; private set; }

    [field: SerializeField] public float SpawnRangeMinDown;
    [field: SerializeField] public float SpawnRangeMaxDown;
    [field: SerializeField] public float SpawnRangeMinUp;
    [field: SerializeField] public float SpawnRangeMaxUp;

    public float RandomSpawnTime { get => Random.Range(SpawnTimeMin, SpawnTimeMax); }
}
