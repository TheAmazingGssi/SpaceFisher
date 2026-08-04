using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreBase : Building
{
    [field: SerializeField] public Transform uiTrans {  get; protected set; }
    [field: SerializeField] public StoreData Data {  get; protected set; }

    [SerializeField] protected SpriteRenderer[] spriteRenderers;

    [SerializeField] protected int starterLevel = 0;

    
    public int Level { get; protected set; } = 0;
    public int CurrentValue { get; protected set; }
    public int CurrentPrice { get; protected set; }
    protected override void Start()
    {
        base.Start();
        if (RunManager.Instance.FirstRun)
            Init(Data, starterLevel, true);
    }

    public virtual void Init(StoreData data, int level = 0, bool present = false)
    {
        Level = level; 
        Data = data;
        BuildingType = data.StoreType;
        minInterval = data.MinInterval;
        maxInterval = data.MaxInterval;
        CurrentPrice = data.Price[Level < 2 ? Level + 1 : Level];
        CurrentValue = data.Value[Level];


        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = data.Sprites[Level];
        }

        if (present && !StoresManager.Stores.ContainsKey(this))
            StoresManager.Stores.Add(this, data);
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
        feedback.PlayParticleEffect();
        feedback.SquashStretch();
    }

    protected override void OnFingerUp()
    {
        if (!IsMoving && Level + 1 < Data.Price.Length)
            Bus<StorePressed>.Raise( new StorePressed { Store = this } );
        base.OnFingerUp();
    }

}
