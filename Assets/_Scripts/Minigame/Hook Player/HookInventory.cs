using System.Collections.Generic;
using UnityEngine;

public class HookInventory : MonoBehaviour
{
    Dictionary<FishStats, int> currentlyHookedFish = new Dictionary<FishStats, int>();
    Dictionary<FishStats, Stack<FishAI>> currentlyHookedObjects = new Dictionary<FishStats, Stack<FishAI>>();
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
        AddFish(e.Fish);
        
    }
    void OnMinigameEnd(MinigameEnd e)
    {
        SaveFish();
    }
    private void AddFish(FishAI fishAI)
    {
        fishAmount++;
        if(currentlyHookedFish.ContainsKey(fishAI.Stats))
            currentlyHookedFish[fishAI.Stats]++;
        else
            currentlyHookedFish.Add(fishAI.Stats, 1);

        if (!currentlyHookedObjects.ContainsKey(fishAI.Stats))
            currentlyHookedObjects.Add(fishAI.Stats, new Stack<FishAI>());
        currentlyHookedObjects[fishAI.Stats].Push(fishAI);

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
                RemoveFishVisually(kvp.Key);
                return;
            }
        }
        throw new System.Exception("How did we get here?");
        //The loop above should hit the if once no matter what, guranteeing a return before this exception
        //if you got an exception there the logic is flawed
        //ye dingus
    }
    private void RemoveFishVisually(FishStats fishType)
    {
        FishAI fish = currentlyHookedObjects[fishType].Pop();
        fish.KnockOff();
        
    }
    void SaveFish()
    {
        Inventory.Instance.AddManyFish(currentlyHookedFish);
        currentlyHookedFish.Clear();
        foreach(Stack<FishAI> stack in currentlyHookedObjects.Values)
            stack.Clear();
        fishAmount = 0;
    }
}
