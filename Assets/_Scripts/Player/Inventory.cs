using NativeSerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private SerializableDictionary<string, int> dict = new SerializableDictionary<string, int>();
    private Dictionary<string, int> runtimeDict => dict;
    public Dictionary<FishStats, int> Fish {  
        get 
        {
//#if UNITY_EDITOR
//            string inventory = "This is the current inventory pulled from singleton:\n";
//#endif
            Dictionary<FishStats, int> copy = new Dictionary<FishStats, int>();
            foreach (string id in runtimeDict.Keys)
            {
                if (ScriptablesDatabase.Instance.fishList.ContainsKey(id))
                {
//#if UNITY_EDITOR
//                    inventory += "name: " + ScriptablesDatabase.Instance.fishList[id] + ": " + dict[id] + "\n";
//#endif
                    copy.Add(ScriptablesDatabase.Instance.fishList[id], runtimeDict[id]);
                }
                else
                {
                    Debug.Log("fish that doesnt exist has this UID: " + id);
                }
            }
//#if UNITY_EDITOR
//            Debug.Log(inventory);
//#endif
            return copy;
        } }
    #region Monobehaviour
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadState();
    }
    #endregion
    #region Public Functions
    //Add Fish
    public void AddFish(FishStats fishStats, int amount)
    {
        AddFishNoSave(fishStats, amount);
        SaveState();
    }
    public void AddFish(FishStats fishStats) => AddFish(fishStats, 1);
    public void AddManyFish(Dictionary<FishStats, int> allFish)
    {
        foreach(KeyValuePair<FishStats, int> kvp in allFish)
        {
            AddFishNoSave(kvp.Key, kvp.Value);
        }
        SaveState();
    }
    //Remove Fish
    public bool TryRemoveFish(FishStats fishStats, int amount)
    {
        if (runtimeDict.ContainsKey(fishStats.ID) && runtimeDict[fishStats.ID] >= amount)
        {
            RemoveFish(fishStats, amount);
            return true;
        }
        
        return false;
    }
    public bool TryRemoveFish(FishStats fishStats) => TryRemoveFish(fishStats, 1);

    public bool IsInInventory(FishStats fishStats) => runtimeDict.ContainsKey(fishStats.ID);
    [ContextMenu("Clear Inventory")]
    public void ClearInventory()
    {
        runtimeDict.Clear();
        SaveState();
    }
    #endregion
    #region Private Add Remove Logic
    private void AddFishNoSave(FishStats fishStats, int amount)
    {
        if (amount <= 0) return;

        if (runtimeDict.ContainsKey(fishStats.ID))
        {
            runtimeDict[fishStats.ID] = runtimeDict[fishStats.ID] + amount;
        }
        else
        {
            runtimeDict.Add(fishStats.ID, amount);
        }
        
        //Debug.Log($"Fish name: {fishStats.name} amount in func {amount} amount saved {runtimeDict[fishStats.ID]}");
    }
    private void RemoveFish(FishStats fishStats, int amount)
    {
        runtimeDict[fishStats.ID] -= amount;
        if(runtimeDict[fishStats.ID] <= 0)
        {
            runtimeDict.Remove(fishStats.ID);
            Bus<FishInventoryChange>.Raise(new FishInventoryChange { Fish = fishStats});
        }
        SaveState();
        //DebugPrintDictionary();
    }
    private void DebugPrintDictionary()
    {
        string print = "";
        foreach (string id in runtimeDict.Keys)
        {
            print += id + ": " + runtimeDict[id] + "\n";
        }
        Debug.Log(print);
    }
#endregion
    #region Save Load Jason
    private void SaveState()
    {
        string jsonFile = JsonUtility.ToJson(runtimeDict);
        string path = Application.persistentDataPath + Constants.Paths.InventoryPath;
        System.IO.File.WriteAllText(path, jsonFile);
    }
    private void LoadState()
    {
        string path = Application.persistentDataPath + Constants.Paths.InventoryPath;
        if (!System.IO.File.Exists(path)) return;

        string jsonFile = System.IO.File.ReadAllText(path);
        dict = JsonUtility.FromJson<SerializableDictionary<string, int>>(jsonFile);

        List<string> toRemove = new List<string>();
        foreach (KeyValuePair<string, int> kvp in runtimeDict)
            if (kvp.Value <= 0)
                toRemove.Add(kvp.Key);

        foreach (string key in toRemove)
            runtimeDict.Remove(key);
    }
    #endregion
}
