using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StoresManager : MonoBehaviour
{
    public static List<StoreData> Stores { get; private set; } = new List<StoreData>();

    [SerializeField] private List<StoreData> startingStores = new List<StoreData>();

    private void Start()
    {

        Stores = startingStores;
    }
}
