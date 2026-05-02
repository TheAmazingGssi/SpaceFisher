using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Aquarium : ClickableObject
{
    public List<FishManager> Fish { get; private set; }

    protected override void OnFingerUp()
    {
        Debug.Log("Aquarium pressed");
        Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = this });
    }

    public void AddFish(FishManager fish)
    {
        Debug.Log("Fish Added");
    }
}