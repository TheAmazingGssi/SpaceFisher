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

    protected override void OnVisitorAdded(Visitor visitor)
    {
        CoinsManager.Instance.AddCoins(data.Price);
    }
}