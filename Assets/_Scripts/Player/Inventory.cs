using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<FishStats> Fish { get; private set; } = new List<FishStats>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddFish(FishStats fishStats)
    {
        Fish.Add(fishStats);
    }

    public void RemoveFish(FishStats fishStats)
    {
        Fish.Remove(fishStats);
    }

    public void ClearInventory()
    {
        Fish.Clear();
    }



}
