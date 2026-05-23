using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Store : Building
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BuildingData data;

    public void Init(BuildingData data)
    {
        this.data = data;
        spriteRenderer.sprite = data.Sprite;
        StoreType = data.StoreType;
        minInterval = data.MinInterval;
        maxInterval = data.MaxInterval;
        StartReleaseRoutine();
    }

    protected override void OnVisitorAdded(Visitor visitor)
    {
        CoinsManager.Instance.AddCoins(data.Price);
    }

    protected override void ReleaseVisitor(Visitor visitor)
    {
        Bus<VisitorReleased>.Raise(new VisitorReleased { Visitor = visitor, Store = this });
    }
}