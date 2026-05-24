using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private List<Visitor> visitors = new List<Visitor>();

    [SerializeField] private Transform[] floors;

    public void AddVisitor(Visitor visitor)
    {
        visitors.Add(visitor);
        visitor.gameObject.SetActive(false);
    }

    public void RemoveVisitor(Visitor visitor)
    {
        visitors.Remove(visitor);
    }
}
