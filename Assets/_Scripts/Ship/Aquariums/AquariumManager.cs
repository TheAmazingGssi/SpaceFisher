using System.Collections.Generic;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    private List<Aquarium> aquariums = new List<Aquarium>();

    private Aquarium currentAquarium;

    private void Start()
    {
        Bus<AquariumPressed>.OnEvent += SetCurrentAquarium;
        Bus<PlaceFish>.OnEvent += AddFish;
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
