using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Store : Building
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        if(data)
            Init(data);
    }

    public void Init(BuildingData data)
    {
        this.data = data;
        spriteRenderer.sprite = data.Sprite;
        BuildingType = data.StoreType;
        minInterval = data.MinInterval;
        maxInterval = data.MaxInterval;
    }

    public override Vector2 GetEntryPoint(Collider2D col, Vector2 visitorPos)
    {
        return new Vector2(col.bounds.center.x, visitorPos.y);
    }

    protected override void OnVisitorAdded(Visitor visitor)
    {
        CoinsManager.Instance.AddCoins(data.Price);
    }
}