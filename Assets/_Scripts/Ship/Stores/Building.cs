using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MoveableObject
{
    [SerializeField] protected FeedbackManager feedback;

    public Queue<Visitor> Visitors = new Queue<Visitor>();
    public Location BuildingType { get; protected set; }

    protected Coroutine releaseRoutine;
    protected float minInterval;
    protected float maxInterval;

    protected virtual void Start()
    {
        Bus<ChangeLocation>.OnEvent += AddVisitor;
    }

    public virtual Vector2 GetEntryPoint(Collider2D col, Vector2 visitorPos)
    {
        return new Vector2(Random.Range(col.bounds.min.x, col.bounds.max.x), visitorPos.y);
    }
    protected void StartReleaseRoutine()
    {
        if (releaseRoutine != null) return;
        releaseRoutine = StartCoroutine(ReleaseRoutine());
    }

    private void AddVisitor(ChangeLocation e)
    {
        if (e.Building != this) return;
        Visitors.Enqueue(e.Visitor);
        StartReleaseRoutine();
        OnVisitorAdded(e.Visitor);
    }

    private IEnumerator ReleaseRoutine()
    {
        while (Visitors.Count > 0)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            if (Visitors.Count > 0)
                ReleaseVisitor(Visitors.Dequeue());
        }
        releaseRoutine = null;
    }
    protected virtual void ReleaseVisitor(Visitor visitor)
    {
        Bus<VisitorReleased>.Raise(new VisitorReleased { Visitor = visitor, Building = this });
    }
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
