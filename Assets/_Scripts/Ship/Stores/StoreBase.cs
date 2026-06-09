using UnityEngine;

public class StoreBase : Building
{
    [field: SerializeField] public StoreData Data;
    [SerializeField] protected SpriteRenderer[] spriteRenderers;

    [Header("UI")]
    public SpriteRenderer[] SpriteRenderers;
    public int Level { get; private set; } = 1;
    public int CurrentValue { get; private set; }
    public int CurrentPrice { get; private set; }
    protected override void Start()
    {
        base.Start();
        if (Data)
            Init(Data);
    }

    public virtual void Init(StoreData data)
    {
        Data = data;
        BuildingType = data.StoreType;
        minInterval = data.MinInterval;
        maxInterval = data.MaxInterval;
        Level = data.Level;

        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].sprite = data.Sprites[0];

    }

    public virtual void Upgrade(int lvl)
    {
        Level = lvl;
        CurrentPrice = Data.Price[Level];
        CurrentValue = Data.Value[Level];

        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].sprite = Data.Sprites[Level];
    }

    protected override void OnVisitorAdded(Visitor visitor)
    {
        CoinsManager.Instance.AddCoins(Data.Value[Level]);
    }

    protected override void OnFingerUp()
    {
        base.OnFingerUp();
    }
}
