using System.Collections.Generic;
using UnityEngine;
public class Aquarium : ClickableObject
{
    [SerializeField] private Transform fishSpawn;
    public List<FishManager> Fish { get; private set; }
    [SerializeField] MoveableObject MoveableObjectComponent;

    private AquariumFishPool pool;

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
        if (!MoveableObjectComponent.IsMoving)
            Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = this });
        else
            MoveableObjectComponent.TryStopMoving();
    }
    protected override void OnFingerHold()
    {
        base.OnFingerHold();
        if (PlacementManager.Instance.CanTakeObject)
        {
            MoveableObjectComponent.StartMoving();
        }
    }

    public void AddFish(FishManager fish)
    {   
        FishManager newFish = pool.Get(fish);
        newFish.transform.parent = fishSpawn;
        newFish.transform.position = fishSpawn.position;
        Fish.Add(newFish);
    }
}