using System.Collections.Generic;
using UnityEngine;
public class Aquarium : ClickableObject
{
    [SerializeField] private Transform fishSpawn;
    public List<FishManager> Fish { get; private set; }

    private AquariumFishPool pool;

    bool moving = false;

    private void Start()
    {
        Fish = new List<FishManager>(); 
    }

    public void Initialize(AquariumFishPool pool)
    {
        this.pool = pool;
    }

    protected override void OnFingerUp()
    {
        if (!moving)
            Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = this });
        moving = false;
    }
    protected override void OnFingerHold()
    {
        base.OnFingerHold();
        PlacementManager.Instance.CurrentlyMovingObject = gameObject;
        moving = true;
    }

    public void AddFish(FishManager fish)
    {   
        FishManager newFish = pool.Get(fish);
        newFish.transform.parent = fishSpawn;
        newFish.transform.position = fishSpawn.position;
        Fish.Add(newFish);
    }
}