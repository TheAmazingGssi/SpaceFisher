using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Aquarium : Building
{
    [SerializeField] private Transform fishSpawn;
    public List<FishManager> Fish { get; private set; }
    private AquariumFishPool pool;

    protected override void Start()
    {
        base.Start();
        Fish = new List<FishManager>();
        BuildingType = Location.Aquarium;
        minInterval = data.MinInterval;
        maxInterval = data.MaxInterval;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        AquariumManager.Aquariums.Add(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        AquariumManager.Aquariums.Remove(this);
    }

    public override Vector2 GetEntryPoint(Collider2D col, Vector2 visitorPos)
    {
        return new Vector2(Random.Range(col.bounds.min.x, col.bounds.max.x), visitorPos.y);
    }

    public void Init(AquariumFishPool pool)
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
}