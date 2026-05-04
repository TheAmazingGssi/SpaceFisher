using UnityEngine;

[CreateAssetMenu(fileName = "MinigameRules", menuName = "Scriptable Objects/MinigameRules")]
public class MinigameRules : ScriptableObject
{
    [field: SerializeField] public float SpawnDistanceMin { get; private set; }
    [field: SerializeField] public float SpawnDistanceMax { get; private set; }
    public float RandomSpawnDistance { get => Random.Range(SpawnDistanceMin, SpawnDistanceMax); }
}
