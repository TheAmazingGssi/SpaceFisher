using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class TouchState
{
    private const float DragThreshold = 20;

    private static readonly Dictionary<int, float> totalMovement = new();
    private static readonly HashSet<int> draggedFingers = new();
    private static readonly HashSet<int> claimedFingers = new();
    public static void Claim(int fingerId)
    {
        claimedFingers.Add(fingerId);
        totalMovement[fingerId] = 0;
        draggedFingers.Remove(fingerId);
    }
    public static void Release(int fingerId)
    {
        claimedFingers.Remove(fingerId);
        totalMovement.Remove(fingerId);
        draggedFingers.Remove(fingerId);
    }

    public static bool IsClaimed(int fingerId) => claimedFingers.Contains(fingerId);
    public static void RegisterMovement(int fingerId, float deltaDistance)
    {
        if (!totalMovement.ContainsKey(fingerId)) return;
        totalMovement[fingerId] += deltaDistance;
        if (totalMovement[fingerId] >= DragThreshold)
            draggedFingers.Add(fingerId);
    }

    public static void RegisterFingerDown(int fingerId)
    {
        totalMovement[fingerId] = 0;
        draggedFingers.Remove(fingerId);
    }

    public static void ClearFinger(int fingerId)
    {
        totalMovement.Remove(fingerId);
        draggedFingers.Remove(fingerId);
    }

    public static bool WasDragged(int fingerId) => draggedFingers.Contains(fingerId);

    public static bool IsOverUI(Vector2 screenPos)
    {
        if (EventSystem.current == null) return false;
        var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results.Count > 0;
    }
}