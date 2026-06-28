using NativeSerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private SerializableDictionary<string, int> dict = new SerializableDictionary<string, int>();
    public Dictionary<FishStats, int> Fish {  
        get 
        { 
            Dictionary<FishStats, int> copy = new Dictionary<FishStats, int>();
            foreach (string id in dict.Keys)
            {
                copy.Add(ScriptablesDatabase.Instance.fishList[id], dict[id]);
            }
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
        if (dict.ContainsKey(fishStats.ID) && dict[fishStats.ID] >= amount)
        {
            RemoveFish(fishStats, amount);
            return true;
        }
        
        return false;
    }
    public bool TryRemoveFish(FishStats fishStats) => TryRemoveFish(fishStats, 1);

    public bool IsInInventory(FishStats fishStats) => dict.ContainsKey(fishStats.ID);
    [ContextMenu("Clear Inventory")]
    public void ClearInventory()
    {
        dict.Clear();
        SaveState();
    }
    #endregion
    #region Private Add Remove Logic
    private void AddFishNoSave(FishStats fishStats, int amount)
    {
        if (amount <= 0) return;

        if (dict.ContainsKey(fishStats.ID))
            dict[fishStats.ID] += amount;
        else
            dict.Add(fishStats.ID, amount);

        //DebugPrintDictionary();
    }
    private void RemoveFish(FishStats fishStats, int amount)
    {
        dict[fishStats.ID] -= amount;
        if(dict[fishStats.ID] <= 0)
        {
            dict.Remove(fishStats.ID);
            Bus<FishInventoryChange>.Raise(new FishInventoryChange { Fish = fishStats});
        }
        SaveState();
        //DebugPrintDictionary();
    }
    private void DebugPrintDictionary()
    {
        string print = "";
        foreach (string id in dict.Keys)
        {
            print += id + ": " + dict[id] + "\n";
        }
        Debug.Log(print);
    }
#endregion
    #region Save Load Jason
    private void SaveState()
    {
        string jsonFile = JsonUtility.ToJson(dict);
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
        foreach (KeyValuePair<string, int> kvp in dict)
            if (kvp.Value <= 0)
                toRemove.Add(kvp.Key);

        foreach (string key in toRemove)
            dict.Remove(key);
    }
    #endregion
}
