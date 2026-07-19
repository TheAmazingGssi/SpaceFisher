using System.Collections.Generic;
using UnityEngine;

public class HookInventory : MonoBehaviour
{
    Dictionary<FishStats, int> currentlyHookedFish = new Dictionary<FishStats, int>();
    int fishAmount = 0;

    private void OnEnable()
    {
        Bus<FishCaught>.OnEvent += OnFishCaught;
        Bus<FishFell>.OnEvent += RemoveRandomFish;
        Bus<MinigameEnd>.OnEvent += OnMinigameEnd;
    }
    private void OnDisable()
    {
        Bus<FishCaught>.OnEvent -= OnFishCaught;
        Bus<FishFell>.OnEvent -= RemoveRandomFish;
        Bus<MinigameEnd>.OnEvent -= OnMinigameEnd;
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
    private void RemoveRandomFish(FishFell e)
    {
        int rand = Random.Range(0, fishAmount);
        foreach(KeyValuePair<FishStats, int> kvp in currentlyHookedFish)
        {
            rand -= kvp.Value;
            if(rand < 0)
            {
                currentlyHookedFish[kvp.Key]--;
                if(currentlyHookedFish[kvp.Key] == 0)
                    currentlyHookedFish.Remove(kvp.Key);
                fishAmount--;
                return;
            }
        }
        throw new System.Exception("How did we get here?");
        //The loop above should hit the if once no matter what, guranteeing a return before this exception
        //if you got an exception there the logic is flawed
        //ye dingus
    }
    void SaveFish()
    {
        Inventory.Instance.AddManyFish(currentlyHookedFish);
        currentlyHookedFish.Clear();
    }
}
