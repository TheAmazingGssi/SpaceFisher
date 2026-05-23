using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Aquarium : Building
{
    [SerializeField] private Transform fishSpawn;
    [SerializeField] private BuildingData data;

    public List<FishManager> Fish { get; private set; }
    private AquariumFishPool pool;

    protected override void Start()
    {
        base.Start();
        Fish = new List<FishManager>();
        StoreType = Location.Aquarium;
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

    public void Initialize(AquariumFishPool pool)
    {
        this.pool = pool;
        StartReleaseRoutine();
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

    protected override void ReleaseVisitor(Visitor visitor)
    {
        visitor.gameObject.SetActive(true);
        Bus<VisitorReleased>.Raise(new VisitorReleased { Visitor = visitor });
    }

    public void AddFish(FishManager fish)
    {
        FishManager newFish = pool.Get(fish);
        newFish.transform.parent = fishSpawn;
        newFish.transform.position = fishSpawn.position;
        Fish.Add(newFish);
    }
}