using System.Collections.Generic;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    [SerializeField] private AquariumFishPool pool;
    [SerializeField] private List<Aquarium> aquariums = new List<Aquarium>();

    private Aquarium currentAquarium;

    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent += AddFish;

        foreach(Aquarium aquarium in aquariums)
        {
            aquarium.Initialize(pool);
        }
    }

    private void SetCurrentAquarium(AquariumPressed e)
    {
        currentAquarium = e.Aquarium;
    }

    private void AddFish(PlaceFish e)
    {
        currentAquarium.AddFish(e.Fish);
    }

    private void OnDestroy()
    {
        Bus<AquariumPressed>.OnEvent -= SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent -= AddFish;
    }
}
