using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoreBase : MonoBehaviour
{
    public Queue<Visitor> Visitors = new Queue<Visitor>();
    [SerializeField] private int coins = 5;
    [SerializeField] private float minInterval = 5;
    [SerializeField] private float maxInterval = 15;

    private Coroutine _releaseRoutine;

    public abstract Store Zone { get; }

    private void Start()
    {
        Bus<ChangeLocation>.OnEvent += AddVisitor;
        _releaseRoutine = StartCoroutine(ReleaseRoutine());
    }

    protected virtual void AddVisitor(ChangeLocation e)
    {
        if (e.Visitor.CurrentStore != Zone) return;
        Visitors.Enqueue(e.Visitor);
        StopCoroutine(_releaseRoutine);
        _releaseRoutine = StartCoroutine(ReleaseRoutine());
        NewVisitor(e.Visitor);
        CoinsManager.Instance.AddCoins(coins);
    }

    private IEnumerator ReleaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            if (Visitors.Count > 0)
                Bus<VisitorReleased>.Raise(new VisitorReleased { Visitor = Visitors.Dequeue() });
        }
    }

    protected abstract void NewVisitor(Visitor visitor);

    private void OnDestroy()
    {
        Bus<ChangeLocation>.OnEvent -= AddVisitor;
    }
}