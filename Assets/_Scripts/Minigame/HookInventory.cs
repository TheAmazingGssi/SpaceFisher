using System.Collections.Generic;
using UnityEngine;

public class HookInventory : MonoBehaviour
{
    Dictionary<FishStats, int> currentlyHookedFish = new Dictionary<FishStats, int>();
    int fishAmount = 0;

    private void OnEnable()
    {
        Bus<FishCaught>.OnEvent += OnFishCaught;
        Bus<MinigameEnd>.OnEvent += OnMinigameEnd;
    }

    void OnFishCaught(FishCaught e)
    {
        AddFish(e.Fish.Stats);
    }
    void OnMinigameEnd(MinigameEnd e)
    {
        SaveFish();
    }
    private void AddFish(FishStats fishStats)
    {
        fishAmount++;
        if(currentlyHookedFish.ContainsKey(fishStats))
            currentlyHookedFish[fishStats]++;
        else
            currentlyHookedFish.Add(fishStats, 1);
    }
    void SaveFish()
    {
        Inventory.Instance.AddManyFish(currentlyHookedFish);
        currentlyHookedFish.Clear();
    }
}
