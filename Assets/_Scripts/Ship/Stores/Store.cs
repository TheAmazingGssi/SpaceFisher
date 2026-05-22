using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Store : MoveableObject
{
    public Queue<Visitor> Visitors = new Queue<Visitor>();
    public StoreType StoreType { get; private set; }

    [SerializeField] private SpriteRenderer spriteRenderer;

    private StoreData data;
    private Coroutine releaseRoutine;

    private void Start()
    {
        Bus<ChangeLocation>.OnEvent += AddVisitor;
    }

    public void Init(StoreData data)
    {
        this.data = data;
        spriteRenderer.sprite = data.Sprite;
        StoreType = data.StoreType;
        releaseRoutine = StartCoroutine(ReleaseRoutine());
    }

    private void AddVisitor(ChangeLocation e)
    {
        if (e.Visitor.CurrentStore != StoreType) return;
        Visitors.Enqueue(e.Visitor);
        StopCoroutine(releaseRoutine);
        releaseRoutine = StartCoroutine(ReleaseRoutine());
        NewVisitor(e.Visitor);
        CoinsManager.Instance.AddCoins(data.Price);
    }

    private IEnumerator ReleaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(data.MinInterval, data.MaxInterval));
            if (Visitors.Count > 0)
                Bus<VisitorReleased>.Raise(new VisitorReleased { Visitor = Visitors.Dequeue() });
        }
    }

    private void NewVisitor(Visitor visitor)
    {

    }

    private void OnDisable()
    {
        if (releaseRoutine != null)
        {
            StopCoroutine(releaseRoutine);
            releaseRoutine = null;
        }
        Visitors.Clear();
    }

    private void OnDestroy()
    {
        Bus<ChangeLocation>.OnEvent -= AddVisitor;
    }
}