using System.Collections.Generic;
using UnityEngine;

public interface IEvent { }


#region Aquariums
public struct PlaceFish : IEvent { public FishStats Fish; }
public struct AquariumPressed : IEvent { public Aquarium Aquarium; }
public struct AquariumValueChange : IEvent { public float Value; }
public struct AquariumPriceChange : IEvent { public int Price; }
public struct AquariumBought : IEvent { }
#endregion

#region Shop
public struct StoreBought : IEvent { public StoreData Data; }
public struct ShopButtonPressed : IEvent { public ShopButton ShopButton; }
public struct StorePressed : IEvent { public StoreBase Store; }
#endregion

#region Visitors
public struct VisitorSpawned : IEvent { public Visitor Visitor; public int TicketPrice; }
public struct ChangeLocation : IEvent { public Visitor Visitor; public Building Building; }
public struct VisitorReleased : IEvent { public Visitor Visitor; public Building Building; }
public struct VisitorLeaving : IEvent { public Visitor Visitor; }
#endregion

#region Minigame
public struct FishCaught : IEvent { public FishAI Fish; }
public struct FishFell : IEvent { };
public struct MinigameStart : IEvent { };
public struct MinigameEnd : IEvent { };
#endregion

#region Coins
public struct CoinChange : IEvent { public int NewCoins; }
public struct ShowCoin : IEvent { public Transform Transform; }

#endregion 

#region Fish
public struct FishInventoryChange : IEvent { public FishStats Fish; }
#endregion 
#region Upgrades
public struct UpgradeBought : IEvent{ public Upgrade Upgrade; public int NewLevel;}
#endregion 

#region System
public struct OfflineTimeCalculated : IEvent { public float SecondsOffline; }
public struct SaveData : IEvent { }
public struct LoadData : IEvent { public GameSaveData Data; }
public struct RestoreScene : IEvent { public List<StoreSaveData> Stores; public List<AquariumSaveData> Aquariums; }
#endregion 
