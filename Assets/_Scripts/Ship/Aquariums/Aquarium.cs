using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Aquarium : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    public List<FishManager> Fish { get; private set; }

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnEnable()
    {
        Touch.onFingerDown += OnFingerDown;
    }

    private void OnFingerDown(Finger finger)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);

        if (_collider == Physics2D.OverlapPoint(worldPos))
        {
            Debug.Log("Aquarium pressed");
            Bus<AquariumPressed>.Raise(new AquariumPressed { Aquarium = this });
        }
    }

    public void AddFish(FishManager fish)
    {
        Debug.Log("Fish Added");
    }
    private void OnDisable()
    {
        Touch.onFingerDown -= OnFingerDown;
    }
}