using System.Collections.Generic;
using UnityEngine;

public static class MyDebug 
{
    static public void PrintFishDictionary(string note, Dictionary<FishStats, int> dict)
    {
        string inventory = note + "\n";
        foreach (FishStats fish in dict.Keys)
        {
            inventory += "name: " + fish.name + ": " + dict[fish] + "\n";
        }
        Debug.Log(inventory);
    }
}
