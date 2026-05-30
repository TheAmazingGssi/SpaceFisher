using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public int Coins;
    public List<AquariumSaveData> Aquariums = new List<AquariumSaveData>();
    public List<StoreSaveData> Stores = new List<StoreSaveData>();
}

[Serializable]
public class AquariumSaveData
{
    public float XPos;
    public float YPos;
    public List<string> FishIds = new List<string>();

    public Vector2 Position => new Vector2(XPos, YPos);
}

[Serializable]
public class StoreSaveData
{
    public string BuildingDataId;
    public float XPos;
    public float YPos;

    public Vector2 Position => new Vector2(XPos, YPos);
}