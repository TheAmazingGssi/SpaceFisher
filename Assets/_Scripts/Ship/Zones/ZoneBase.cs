using System.Collections.Generic;
using UnityEngine;

public abstract class ZoneBase : MonoBehaviour
{
    public List<Visitor> Visitors = new List<Visitor>();

    public abstract Zone Zone { get; }

    private void Start()
    {
        Bus<ChangeLocation>.OnEvent += AddVisitor;
    }

    protected virtual void AddVisitor(ChangeLocation e)
    {
        if (Visitors.Contains(e.Visitor)) Visitors.Remove(e.Visitor);
        if (e.Visitor.CurrentZone != Zone) return;
        Visitors.Add(e.Visitor);
    }

    private void OnDestroy()
    {
        Bus<ChangeLocation>.OnEvent -= AddVisitor;
    }
}
