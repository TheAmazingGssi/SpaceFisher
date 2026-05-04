using UnityEngine;

public interface IEvent { }

public struct PlaceFish : IEvent { public FishManager Fish; }
public struct AquariumPressed : IEvent { public Aquarium Aquarium; }
public struct FishCaught : IEvent { public FishAI Fish; }
