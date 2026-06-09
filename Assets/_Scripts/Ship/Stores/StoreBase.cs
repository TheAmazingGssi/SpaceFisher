using UnityEngine;

public class StoreBase : Building
{
    [field: SerializeField] public Transform uiTrans {  get; protected set; }
    [field: SerializeField] public StoreData Data {  get; protected set; }

    [SerializeField] protected SpriteRenderer[] spriteRenderers;

    
    public int Level { get; protected set; } = 1;
    public int CurrentValue { get; protected set; }
    public int CurrentPrice { get; protected set; }
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
        CurrentPrice = data.Price[Level + 1];
        CurrentValue = data.Value[Level];


        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].sprite = data.Sprites[0];

    }

    [ContextMenu("UpgradeStore")]
    public virtual void Upgrade()
    {
        Level++;
        CurrentValue = Data.Value[Level];

        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].sprite = Data.Sprites[Level];
        if (Level + 1 < Data.Price.Length)
            CurrentPrice = Data.Price[Level + 1];
    }

    protected override void OnVisitorAdded(Visitor visitor)
    {
        CoinsManager.Instance.AddCoins(CurrentValue);
    }

    protected override void OnFingerUp()
    {
        if (!IsMoving && Level + 1 < Data.Price.Length)
            Bus<StorePressed>.Raise( new StorePressed { Store = this } );
        else
            Debug.Log("Level: " +  Level + "Price length: " + Data.Price.Length);
        base.OnFingerUp();
    }

}
