using UnityEngine;

public interface IEvent { }


#region Aquariums
public struct PlaceFish : IEvent { public FishManager Fish; }
public struct AquariumPressed : IEvent { public Aquarium Aquarium; }
public struct AquariumValueChange : IEvent { public float Value; }
public struct AquariumPriceChange : IEvent { public int Price; }
public struct AquariumBought : IEvent { }
#endregion

#region Stores
public struct StoreBought : IEvent { public BuildingData Data; }
#endregion

#region Visitors
public struct VisitorSpawned : IEvent { public Visitor Visitor; public int TicketPrice; }
public struct ChangeLocation : IEvent { public Visitor Visitor; }
public struct VisitorReleased : IEvent { public Visitor Visitor; public Store Store; }
#endregion

#region Minigame
public struct FishCaught : IEvent { public FishAI Fish; }
public struct MinigameStart : IEvent { };
public struct MinigameEnd : IEvent { };
#endregion

#region Coins
public struct CoinChange : IEvent { public int NewCoins; }
#endregion 

#region System
public struct OfflineTimeCalculated : IEvent { public float SecondsOffline; }
#endregion 
