using UnityEngine;

public interface IEvent { }


#region Aquariums
public struct PlaceFish : IEvent { public FishManager Fish; }
public struct AquariumPressed : IEvent { public Aquarium Aquarium; }
#endregion

#region Zones
public struct RatingChanged : IEvent { public ZoneBase Zone; }
#endregion

#region Visitors
public struct VisitorSpawned : IEvent { public Visitor Visitor; }
public struct ChangeLocation : IEvent { public Visitor Visitor; }
#endregion

#region Minigame
public struct FishCaught : IEvent { public FishAI Fish; }
public struct MinigameStart : IEvent { };
public struct MinigameEnd : IEvent { };
#endregion
