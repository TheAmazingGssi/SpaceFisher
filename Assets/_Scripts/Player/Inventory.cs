using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const string INVENTORY_PATH = "/fishInventory.json";
    public static Inventory Instance;

    public Dictionary<string, int> dict = new Dictionary<string, int>();

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

    public void AddFish(FishStats fishStats, int amount)
    {
        if(dict.ContainsKey(fishStats.ID))
            dict[fishStats.ID] += amount;
        else
            dict.Add(fishStats.ID, amount);
        SaveState();
    }
    public void AddFish(FishStats fishStats) => AddFish(fishStats, 1);
    public void RemoveFish(FishStats fishStats, int amount)
    {
        if (dict.ContainsKey(fishStats.ID) && dict[fishStats.ID] >= amount)
            dict[fishStats.ID] -= amount;
        else
            Debug.LogError("Tried removing a fish the player did not have");
        SaveState();
    }
    public void RemoveFish(FishStats fishStats) => RemoveFish(fishStats, 1);
    public void ClearInventory()
    {
        dict.Clear();
        SaveState();
    }


    private void SaveState()
    {
        string jsonFile = "{\n";
        foreach (KeyValuePair<string, int> kvp in dict)
        {
            jsonFile += $"\"{kvp.Key}\": {kvp.Value}\n";
        }
        jsonFile += "}";
        string path = Application.persistentDataPath + INVENTORY_PATH;
        System.IO.File.WriteAllText(path, jsonFile);
        Debug.Log(jsonFile);
    }
    private void LoadState()
    {
        string jsonFile = System.IO.File.ReadAllText(Application.persistentDataPath + INVENTORY_PATH);
        dict = JsonUtility.FromJson<Dictionary<string, int>>(jsonFile);
        Debug.Log(jsonFile);
    }
}
