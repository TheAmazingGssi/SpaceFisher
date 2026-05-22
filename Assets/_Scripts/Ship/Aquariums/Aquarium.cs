using System.Collections.Generic;
using UnityEngine;
public class Aquarium : MoveableObject
{
    [SerializeField] private Transform fishSpawn;
    public List<FishManager> Fish { get; private set; }

    private AquariumFishPool pool;

    private void Start()
    {
        Fish = new List<FishManager>(); 
    }

    private void OnEnable()
    {
        AquariumManager.Aquariums.Add(this);
    }

    public void Initialize(AquariumFishPool pool)
    {
        this.pool = pool;
    }

    public void NewAquarium()
    {
        TryStartMoving();
    }

    protected override void OnFingerUp()
    {
        if (!IsMoving)
            Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = this });
        base.OnFingerUp();
    }

    public void AddFish(FishManager fish)
    {   
        FishManager newFish = pool.Get(fish);
        newFish.transform.parent = fishSpawn;
        newFish.transform.position = fishSpawn.position;
        Fish.Add(newFish);
    }

    private void OnDisable()
    {
        AquariumManager.Aquariums.Remove(this);
    }
}