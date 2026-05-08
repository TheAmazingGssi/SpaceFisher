using System.Collections.Generic;
using UnityEngine;

public class VisitorsManager : MonoBehaviour
{
    [SerializeField] private VisitorSpawner spawner;
    [SerializeField] private ZoneBase[] zones;

    public static List<Visitor> Visitors = new List<Visitor>();

    
}
