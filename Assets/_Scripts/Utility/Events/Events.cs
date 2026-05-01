using UnityEngine;

public interface IEvent { }

public struct PlaceFish : IEvent { public FishManager Fish; }
public struct TestEvent : IEvent { }

