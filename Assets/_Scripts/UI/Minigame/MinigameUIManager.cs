using NativeSerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameUIManager : MonoBehaviour
{
    [Header("Game Objects followed")]
    [SerializeField] Transform playerHook;
    [Header("Out Of Minigame")]
    [SerializeField] GameObject outofMinigameParent;
    [SerializeField] SerializableDictionary<FishStats, Image> siluetteImages;
    [Header("Inside Minigame")]
    [SerializeField] GameObject insideMinigameParent;
    [SerializeField] TextMeshProUGUI depthText;

    private void OnEnable()
    {
        Bus<MinigameStart>.OnEvent += OnMinigameStart;
        Bus<MinigameEnd>.OnEvent += OnMinigameEnd;
    }
    private void OnDisable()
    {
        Bus<MinigameStart>.OnEvent -= OnMinigameStart;
        Bus<MinigameEnd>.OnEvent -= OnMinigameEnd;
    }
    private void Start()
    {
        ActiveOOM();
        insideMinigameParent.SetActive(false);
    }
    private void Update()
    {
        int height = -(int)playerHook.position.y;
        depthText.text = height.ToString();
    }

    private void OnMinigameStart(MinigameStart e)
    {
        StartCoroutine(SetMenuActiveNextFrame(false));
        insideMinigameParent.SetActive(true);
    }
    private void OnMinigameEnd(MinigameEnd e)
    {
        StartCoroutine(SetMenuActiveNextFrame(true));
        insideMinigameParent.SetActive(false);
    }

    public void GoToAquariumScene()
    {
        SceneManager.LoadScene(Constants.Scenes.Ship);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(Constants.Scenes.Minigame);
    }
    IEnumerator SetMenuActiveNextFrame(bool value)
    {
        yield return null;
        if(value)
            ActiveOOM();
        else
            outofMinigameParent.SetActive(value);
    }

    private void ActiveOOM()
    {
        outofMinigameParent.SetActive(true);
        foreach(FishStats kvp in siluetteImages.Keys)
        {
            Dictionary<FishStats, int> fishInInventory = Inventory.Instance.Fish;
            if (fishInInventory.ContainsKey(kvp) && fishInInventory[kvp] > 0)
                siluetteImages[kvp].color = Color.white;
            else
                siluetteImages[kvp].color = Color.black;
        }
    }
}
