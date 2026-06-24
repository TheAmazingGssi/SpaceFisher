using System;
using UnityEngine;

[Serializable]
public struct FishSpawnData
{
    [SerializeField] public FishStats fishType;
    [SerializeField] public float minHeight;
    [SerializeField] public float populationPeak;
    [SerializeField] public float maxHeight;
}

[CreateAssetMenu(fileName = "PlanetFishTable", menuName = "Scriptable Objects/PlanetFishTable")]
public class PlanetFishTable : ScriptableObject
{
    public Planet Planet { get => planet;  }
    [SerializeField] private Planet planet;
    public float MaxDepth { get => maxDepth; }
    [SerializeField] private float maxDepth;
    public Gradient BackgroundGradient;
    public FishSpawnData[] FishSpawnData { get => fishSpawnData; } //TODO: copy or smth to protect the original data
    [SerializeField] private FishSpawnData[] fishSpawnData;
}
