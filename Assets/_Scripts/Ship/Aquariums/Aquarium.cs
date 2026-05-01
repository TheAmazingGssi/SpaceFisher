using System.Collections.Generic;
using UnityEngine;

public class Aquarium : MonoBehaviour
{
    public List<FishManager> Fish {  get; private set; }

    private void Start()
    {
        Fish = new List<FishManager>();
    }
}
