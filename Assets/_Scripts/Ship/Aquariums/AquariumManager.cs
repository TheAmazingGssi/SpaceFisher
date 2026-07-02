using System.Collections.Generic;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    [SerializeField] private AquariumFishPool fishPool;
    [SerializeField] private AquariumPool aquariumPool;
    [SerializeField] private int price = 300;
    public static List<Aquarium> Aquariums = new List<Aquarium>();
    private List<FishStats> fishInAquariums = new List<FishStats>();
    private Aquarium currentAquarium;

    private void Awake()
    {
        Bus<AquariumPressed>.OnEvent += SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent += AddFish;
        Bus<AquariumBought>.OnEvent += AddAquarium;        
    }

    private void Start()
    {
        foreach (Aquarium aquarium in Aquariums)
            aquarium.Init(fishPool);
        Bus<AquariumPriceChange>.Raise(new AquariumPriceChange { Price = price });
#if UNITY_EDITOR
        if (!PlayerPrefs.HasKey(Constants.FirstOpen))
        {
            PlayerPrefs.SetInt(Constants.FirstOpen, 1);
            PlayerPrefs.Save();
        }
        else
        {
            RestoreAquariums();
        }
#else
        RestoreAquariums();
#endif
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent -= AddFish;
        Bus<AquariumBought>.OnEvent -= AddAquarium;
    }

    private void AddAquarium(AquariumBought e)
    {
        Aquarium aquarium = aquariumPool.Get();
        aquariumPool.SetPosition(aquarium.gameObject);
        Physics2D.SyncTransforms();
        aquarium.Init(fishPool);
        aquarium.NewAquarium();
    }

    private void SetCurrentAquarium(AquariumPressed e) => currentAquarium = e.Aquarium;

    private void AddFish(PlaceFish e)
    {
        currentAquarium.AddFish(e.Fish);
        fishInAquariums.Add(e.Fish);
        Bus<AquariumValueChange>.Raise(new AquariumValueChange { Value = CalculateValue() });
    }

    private void RestoreAquariums()
    {
        foreach (AquariumSaveData ad in RunManager.Instance.GetAquariumStates())
        {
            Aquarium aq = aquariumPool.Get();
            aq.transform.position = ad.Position;
            aq.Init(fishPool);
            SetCurrentAquarium(new AquariumPressed { Aquarium = aq});
            foreach (string fishId in ad.FishIds)
            {
                FishStats stats = ScriptablesDatabase.Instance.fishList[fishId];
                aq.AddFish(stats);
                fishInAquariums.Add(stats);
            }
        }
        Bus<AquariumValueChange>.Raise(new AquariumValueChange { Value = CalculateValue() });
    }

    public float CalculateValue()
    {
        float value = 0;
        foreach (FishStats fish in fishInAquariums)
            value += fish.Value;
        return value;
    }
}