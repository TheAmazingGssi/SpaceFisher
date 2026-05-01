using System.Collections.Generic;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    private List<Aquarium> aquariums = new List<Aquarium>();

    private void Start()
    {
        Bus<TestEvent>.OnEvent += TestEvent;
    }

    private void TestEvent(TestEvent e)
    {
        Debug.Log("Event Raised!");
    }

    private void OnDestroy()
    {
        Bus<TestEvent>.OnEvent -= TestEvent;
    }
}
