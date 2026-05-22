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

    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent += AddFish;
        Bus<AquariumBought>.OnEvent += AddAquarium;

        foreach(Aquarium aquarium in Aquariums)
        {
            aquarium.Initialize(fishPool);
        }

        Bus<AquariumPriceChange>.Raise(new AquariumPriceChange { Price = price });
    }

    private void AddAquarium(AquariumBought e)
    {
        Aquarium aquarium = aquariumPool.Get();
        aquariumPool.SetPosition(aquarium.gameObject);
        aquarium.Initialize(fishPool);
        aquarium.NewAquarium();
    }

    private void SetCurrentAquarium(AquariumPressed e)
    {
        currentAquarium = e.Aquarium;
    }

    private void AddFish(PlaceFish e)
    {
        currentAquarium.AddFish(e.Fish);
        fishInAquariums.Add(e.Fish.Stats);
        Bus<AquariumValueChange>.Raise(new AquariumValueChange{ Value = CalculateValue() });
    }

    public float CalculateValue()
    {
        float value = 0;
        foreach (FishStats fish in fishInAquariums)
            value += fish.Value;
        return value;
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent -= AddFish;
        Bus<AquariumBought>.OnEvent -= AddAquarium;

    }
}
