using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Store : Building
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRendererGreen;
    [SerializeField] private SpriteRenderer spriteRendererRed;
    protected override void Start()
    {
        base.Start();
        if(Data)
            Init(Data);
    }

    public void Init(BuildingData data)
    {
        Data = data;
        spriteRenderer.sprite = data.Sprite;
        spriteRendererGreen.sprite = data.Sprite;
        spriteRendererRed.sprite = data.Sprite;
        BuildingType = data.StoreType;
        minInterval = data.MinInterval;
        maxInterval = data.MaxInterval;
    }

    protected override void OnVisitorAdded(Visitor visitor)
    {
        CoinsManager.Instance.AddCoins(Data.Value);
    }
}