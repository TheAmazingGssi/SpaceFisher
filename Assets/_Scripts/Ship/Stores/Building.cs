using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MoveableObject
{
    public Queue<Visitor> Visitors = new Queue<Visitor>();
    public Location StoreType { get; protected set; }

    protected Coroutine releaseRoutine;
    protected float minInterval;
    protected float maxInterval;

    protected virtual void Start()
    {
        Bus<ChangeLocation>.OnEvent += AddVisitor;
    }

    protected void StartReleaseRoutine()
    {
        if (releaseRoutine != null) StopCoroutine(releaseRoutine);
        releaseRoutine = StartCoroutine(ReleaseRoutine());
    }

    private void AddVisitor(ChangeLocation e)
    {
        if (e.Visitor.CurrentLocation != StoreType) return;
        Visitors.Enqueue(e.Visitor);
        StartReleaseRoutine();
        OnVisitorAdded(e.Visitor);
    }

    private IEnumerator ReleaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            if (Visitors.Count > 0)
                ReleaseVisitor(Visitors.Dequeue());
        }
    }
    protected abstract void ReleaseVisitor(Visitor visitor);
    protected virtual void OnVisitorAdded(Visitor visitor) { }

    override protected void OnDisable()
    {
        if (releaseRoutine != null)
        {
            StopCoroutine(releaseRoutine);
            releaseRoutine = null;
        }
        Visitors.Clear();
        base.OnDisable();
    }

    protected virtual void OnDestroy()
    {
        Bus<ChangeLocation>.OnEvent -= AddVisitor;
    }
}
