using System;
using UnityEngine;

[Serializable]
public struct FishSpawnData
{
    [SerializeField] FishStats fishType;
    [SerializeField] float minHeight;
    [SerializeField] float populationPeak;
    [SerializeField] float maxHeight;
}

[CreateAssetMenu(fileName = "PlanetFishTable", menuName = "Scriptable Objects/PlanetFishTable")]
public class PlanetFishTable : ScriptableObject
{
    public Planet Planet { get => planet;  }
    [SerializeField] private Planet planet;
    public float MaxDepth { get => maxDepth; }
    [SerializeField] private float maxDepth;
    [SerializeField] private FishSpawnData[] fishSpawnData;
}
