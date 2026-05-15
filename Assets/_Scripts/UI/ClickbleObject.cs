using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public abstract class ClickableObject : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;

    private float holdTime = 0.5f;
    private Coroutine holdCoroutine;
    private bool fingerDownOnObject;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnEnable()
    {
        Touch.onFingerDown += HandleFingerDown;
        Touch.onFingerUp += HandleFingerUp;
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= HandleFingerDown;
        Touch.onFingerUp -= HandleFingerUp;
        CancelHold();
    }

    private void HandleFingerDown(Finger finger)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        if (_collider.OverlapPoint(worldPos))
        {
            fingerDownOnObject = true;
            OnFingerDown();
            holdCoroutine = StartCoroutine(HoldRoutine());
        }
    }

    private void HandleFingerUp(Finger finger)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);

        CancelHold();

        if (fingerDownOnObject)
        {
            fingerDownOnObject = false;
            if (_collider.OverlapPoint(worldPos))
                OnFingerUp();
        }
    }

    private IEnumerator HoldRoutine()
    {
        yield return new WaitForSeconds(holdTime);
        OnFingerHold();
    }

    private void CancelHold()
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }
    }

    protected virtual void OnFingerDown() { }
    protected virtual void OnFingerUp() { }
    protected virtual void OnFingerHold() { }
}