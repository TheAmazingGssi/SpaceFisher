using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform fishSection;
    [SerializeField] private FishButton fishButtonPrefab;

    private readonly List<FishButton> buttonPool = new();

    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += RefreshInventory;
    }

    public void RefreshInventory(AquariumPressed e)
    {
        int index = 0;
        foreach (KeyValuePair<FishStats, int> entry in Inventory.Instance.Fish)
        {
            GetButton(index).Setup(entry.Key, entry.Value);
            index++;
        }

        for (int i = index; i < buttonPool.Count; i++)
            buttonPool[i].Hide();
    }

    private FishButton GetButton(int index)
    {
        if (index < buttonPool.Count)
            return buttonPool[index];

        FishButton newButton = Instantiate(fishButtonPrefab, fishSection);
        buttonPool.Add(newButton);
        return newButton;
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= RefreshInventory;
    }
}