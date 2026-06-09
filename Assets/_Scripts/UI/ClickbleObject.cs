using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public abstract class ClickableObject : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;

    private int trackedFingerId = -1;
    private float holdTime = 0.5f;
    private Coroutine holdCoroutine;
    private bool fingerDownOnObject;
    private Camera cam;

    virtual protected void Awake()
    {
        EnhancedTouchSupport.Enable();
        cam = Camera.main;
    }

    virtual protected void OnEnable()
    {
        fingerDownOnObject = false;
        trackedFingerId = -1;
        if (cam == null) cam = Camera.main;
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
        if (cam == null) cam = Camera.main;
        Vector2 worldPos = cam.ScreenToWorldPoint(finger.screenPosition);
        if (_collider.OverlapPoint(worldPos))
        {
            fingerDownOnObject = true;
            trackedFingerId = finger.index;
            OnFingerDown();
            holdCoroutine = StartCoroutine(HoldRoutine());
        }
    }

    private void HandleFingerUp(Finger finger)
    {
        if (finger.index != trackedFingerId) return;
        CancelHold();
        if (IsOverUI(finger.screenPosition)) return;
        if (fingerDownOnObject)
        {
            fingerDownOnObject = false;
            trackedFingerId = -1;
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

        bool hitUI = false;
        foreach (RaycastResult result in results)
            hitUI = hitUI || result.gameObject.layer == Constants.Layers.UI;

        return hitUI;
    }

    protected virtual void OnFingerDown() { Bus<StorePressed>.Raise(new StorePressed()); }
    protected virtual void OnFingerUp() { }
    protected virtual void OnFingerHold() { }
}