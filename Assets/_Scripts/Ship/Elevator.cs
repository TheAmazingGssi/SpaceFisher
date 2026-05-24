using System.Collections.Generic;
using UnityEngine;

public enum ElevatorType { Wall, Middle }

public class Elevator : MonoBehaviour
{
    [SerializeField] private ElevatorType elevatorType;
    [SerializeField] private Vector2 wallDirection;
    [SerializeField] private float spawnOffset = 1;
    [SerializeField] private Transform[] floors;

    private List<Visitor> visitors = new List<Visitor>();

    private void Awake()
    {
        System.Array.Sort(floors, (a, b) => b.position.y.CompareTo(a.position.y));
    }

    public void AddVisitor(Visitor visitor)
    {
        visitors.Add(visitor);
        visitor.gameObject.SetActive(false);

        int currentFloorIndex = GetCurrentFloorIndex(visitor);
        int nextFloorIndex = currentFloorIndex + 1;

        if (nextFloorIndex >= floors.Length)
        {
            ReleaseVisitor(visitor, floors[currentFloorIndex].position, Vector2.left);
            return;
        }

        Vector2 direction = GetExitDirection(nextFloorIndex == floors.Length - 1);
        Vector2 spawnPos = new Vector2(transform.position.x + direction.x * spawnOffset, floors[nextFloorIndex].position.y);
        ReleaseVisitor(visitor, spawnPos, direction);
    }

    private void ReleaseVisitor(Visitor visitor, Vector2 spawnPos, Vector2 direction)
    {
        visitor.transform.position = spawnPos;
        visitor.SetDirectionAndResume(direction);
        visitor.gameObject.SetActive(true);
        visitors.Remove(visitor);
    }

    private int GetCurrentFloorIndex(Visitor visitor)
    {
        int closest = 0;
        float minDistance = Mathf.Abs(visitor.transform.position.y - floors[0].position.y);

        for (int i = 1; i < floors.Length; i++)
        {
            float distance = Mathf.Abs(visitor.transform.position.y - floors[i].position.y);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = i;
            }
        }
        return closest;
    }

    private Vector2 GetExitDirection(bool isFinalFloor)
    {
        if (isFinalFloor) return Vector2.left;

        return elevatorType switch
        {
            ElevatorType.Wall => wallDirection.normalized,
            ElevatorType.Middle => Random.value < 0.5f ? Vector2.left : Vector2.right,
            _ => Vector2.right
        };
    }
}