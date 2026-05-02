using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public abstract class ClickableObject : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnEnable()
    {
        Touch.onFingerDown += HandleFingerDown;
        Touch.onFingerUp += HandleFingerUp;
    }

    private void HandleFingerDown(Finger finger)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        if (_collider.OverlapPoint(worldPos))
            OnFingerDown();
    }

    private void HandleFingerUp(Finger finger)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        if (_collider.OverlapPoint(worldPos))
            OnFingerUp();
    }

    protected virtual void OnFingerDown() { }
    protected virtual void OnFingerUp() { }

    private void OnDisable()
    {
        Touch.onFingerDown -= HandleFingerDown;
        Touch.onFingerUp -= HandleFingerUp;
    }
}