using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField][Range(0.001f, 0.05f)] private float zoomSpeed = 0.01f;
    [SerializeField] private float minSize = 3;
    [SerializeField] private float maxSize = 15;
    [SerializeField] private float panSpeed = 1;
    private const float PanDeadzone = 2;

    private int fingerA = -1;
    private int fingerB = -1;
    private Vector2 prevA;
    private Vector2 prevB;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        if (cam == null) cam = Camera.main;
    }

    private void OnDisable()
    {
        DropFinger(ref fingerA);
        DropFinger(ref fingerB);
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (fingerA != -1 && (FindFinger(fingerA) == null || TouchState.IsClaimed(fingerA)))
            DropFinger(ref fingerA);
        if (fingerB != -1 && (FindFinger(fingerB) == null || TouchState.IsClaimed(fingerB)))
            DropFinger(ref fingerB);

        foreach (var finger in Touch.fingers)
        {
            if (!finger.isActive) continue;
            if (finger.index == fingerA || finger.index == fingerB) continue;
            if (TouchState.IsClaimed(finger.index)) continue;
            if (TouchState.IsOverUI(finger.screenPosition)) continue;

            if (fingerA == -1)
            {
                fingerA = finger.index;
                prevA = finger.screenPosition;
                TouchState.RegisterFingerDown(fingerA);
            }
            else if (fingerB == -1)
            {
                fingerB = finger.index;
                prevB = finger.screenPosition;
                TouchState.RegisterFingerDown(fingerB);
                break;
            }
            else break;
        }

        if (fingerA == -1) return;

        Finger a = FindFinger(fingerA);
        if (a == null) { DropFinger(ref fingerA); return; }

        if (fingerB == -1)
        {
            Vector2 delta = a.screenPosition - prevA;
            if (delta.magnitude > PanDeadzone) Pan(delta);
            TouchState.RegisterMovement(fingerA, delta.magnitude);
            prevA = a.screenPosition;
            return;
        }

        Finger b = FindFinger(fingerB);
        if (b == null) { DropFinger(ref fingerB); prevA = a.screenPosition; return; }

        Vector2 curA = a.screenPosition;
        Vector2 curB = b.screenPosition;
        float delta2 = Vector2.Distance(curA, curB) - Vector2.Distance(prevA, prevB);
        Zoom(-delta2);
        TouchState.RegisterMovement(fingerA, (curA - prevA).magnitude);
        TouchState.RegisterMovement(fingerB, (curB - prevB).magnitude);
        prevA = curA;
        prevB = curB;
    }

    private void DropFinger(ref int fingerId)
    {
        if (fingerId == -1) return;
        TouchState.ClearFinger(fingerId);
        fingerId = -1;
    }

    private static Finger FindFinger(int index)
    {
        foreach (var f in Touch.fingers)
            if (f.index == index && f.isActive) return f;
        return null;
    }

    private void Zoom(float increment) =>
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + increment * zoomSpeed, minSize, maxSize);

    private void Pan(Vector2 screenDelta)
    {
        float worldUnitsPerPixel = cam.orthographicSize * 2f / Screen.height;
        cam.transform.position += new Vector3(-screenDelta.x, -screenDelta.y, 0) * worldUnitsPerPixel * panSpeed;
    }
}