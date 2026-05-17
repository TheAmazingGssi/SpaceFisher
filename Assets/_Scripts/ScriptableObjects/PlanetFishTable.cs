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
    [SerializeField] private Planet planet;
    [SerializeField] private float maxDepth;
    [SerializeField] private FishSpawnData[] fishSpawnData;
}
