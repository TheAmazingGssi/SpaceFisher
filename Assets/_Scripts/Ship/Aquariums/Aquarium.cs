using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aquarium : Building
{
    [field: SerializeField] public AquariumData Data;
    [SerializeField] private Transform fishSpawn;
    public List<FishManager> Fish { get; private set; }
    private AquariumFishPool pool;

    private void Awake()
    {
        Fish = new List<FishManager>();
    }
    protected override void Start()
    {
        base.Start();
        BuildingType = Location.Aquarium;
        minInterval = Data.MinInterval;
        maxInterval = Data.MaxInterval;
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
        WaitForPlacement();
    }

    private Vector2 GetRandomPointInCollider()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector2 point = new Vector2(Random.Range(_collider.bounds.min.x + 0.5f, _collider.bounds.max.x - 0.5f),
                Random.Range(_collider.bounds.min.y + 0.5f, _collider.bounds.max.y - 0.5f));
            if (_collider.OverlapPoint(point))
                return point;
        }

        return _collider.bounds.center;
    }

    protected override void OnFingerUp()
    {
        if (!IsMoving)
            Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = this });
        base.OnFingerUp();
    }

    public void AddFish(FishStats fish, int amount)
    {
        for(int i  = 0; i < amount; i++)
        {
            FishManager newFish = pool.Get(fish);
            newFish.transform.parent = fishSpawn;
            Vector2 spawnPoint = GetRandomPointInCollider();
            newFish.transform.position = spawnPoint;
            newFish.Init(fish);
            feedback.PlayParticleEffect(spawnPoint);
            Fish.Add(newFish);
        }
    }
}