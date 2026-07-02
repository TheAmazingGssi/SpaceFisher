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
    private bool holdTriggered;
    private Vector2 lastTrackedPos;
    private Camera cam;
    virtual protected void OnEnable()
    {
        fingerDownOnObject = false;
        trackedFingerId = -1;
        holdTriggered = false;
        cam = Camera.main;
        Touch.onFingerDown += HandleFingerDown;
        Touch.onFingerUp += HandleFingerUp;
    }
    virtual protected void OnDisable()
    {
        Touch.onFingerDown -= HandleFingerDown;
        Touch.onFingerUp -= HandleFingerUp;
        CancelHold();
    }
    private void Update()
    {
        if (!fingerDownOnObject || trackedFingerId == -1 || holdTriggered) return;

        Finger f = FindFinger(trackedFingerId);
        if (f == null) return;

        Vector2 cur = f.screenPosition;
        float delta = (cur - lastTrackedPos).magnitude;
        lastTrackedPos = cur;
        TouchState.RegisterMovement(trackedFingerId, delta);

        if (TouchState.WasDragged(trackedFingerId))
        {
            CancelHold();
            TouchState.Release(trackedFingerId);
            fingerDownOnObject = false;
            trackedFingerId = -1;
            holdTriggered = false;
            OnClickAbandoned();
        }
    }
    private void HandleFingerDown(Finger finger)
    {
        if (fingerDownOnObject && finger.index != trackedFingerId)
        {
            CancelClick();
            return;
        }

        if (cam == null) cam = Camera.main;
        Vector2 worldPos = cam.ScreenToWorldPoint(finger.screenPosition);
        if (_collider.OverlapPoint(worldPos))
        {
            fingerDownOnObject = true;
            trackedFingerId = finger.index;
            holdTriggered = false;
            lastTrackedPos = finger.screenPosition;
            TouchState.Claim(trackedFingerId);
            OnFingerDown();
            holdCoroutine = StartCoroutine(HoldRoutine());
        }
    }
    private void HandleFingerUp(Finger finger)
    {
        if (finger.index != trackedFingerId) return;
        CancelHold();
        TouchState.Release(trackedFingerId);
        bool overUI = IsOverUI(finger.screenPosition);
        if (fingerDownOnObject)
        {
            fingerDownOnObject = false;
            trackedFingerId = -1;
            holdTriggered = false;
            if (!overUI) OnFingerUp();
        }
    }
    private IEnumerator HoldRoutine()
    {
        yield return new WaitForSeconds(holdTime);
        holdTriggered = true;
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

    private void CancelClick()
    {
        CancelHold();
        if (trackedFingerId != -1)
            TouchState.Release(trackedFingerId);
        fingerDownOnObject = false;
        trackedFingerId = -1;

        if (holdTriggered)
        {
            holdTriggered = false;
            OnFingerUp();
        }
        else
        {
            OnClickAbandoned();
        }
    }
    private static Finger FindFinger(int index)
    {
        foreach (var f in Touch.fingers)
            if (f.index == index && f.isActive) return f;
        return null;
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
    protected virtual void OnClickAbandoned() { }
}