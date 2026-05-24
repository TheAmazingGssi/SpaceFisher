using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public abstract class ClickableObject : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;

    private float holdTime = 0.5f;
    private Coroutine holdCoroutine;
    private bool fingerDownOnObject;

    virtual protected void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    virtual protected void OnEnable()
    {
        Touch.onFingerDown += HandleFingerDown;
        Touch.onFingerUp += HandleFingerUp;
    }

    virtual protected void OnDisable()
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
        Debug.Log("Before over UI");
        if (IsOverUI(finger.screenPosition)) return;
        Debug.Log("After over UI");

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
    private bool IsOverUI(Vector2 screenPos)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);

        pointerData.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }

    protected virtual void OnFingerDown() { }
    protected virtual void OnFingerUp() { }
    protected virtual void OnFingerHold() { }
}