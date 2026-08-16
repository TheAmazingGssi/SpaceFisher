using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElevatorType { Wall, Middle }

public class Elevator : MonoBehaviour
{
    [SerializeField] private ElevatorType elevatorType;
    [SerializeField] private Vector2 wallDirection;
    [SerializeField] private float spawnOffset = 1;
    [SerializeField] private GameObject[] platforms;
    [field: SerializeField] public Transform[] Floors { get; private set; }

    [Header("Platform Movement")]
    [SerializeField] private float platformSpeed = 2;
    [SerializeField] private float minStopTime = 1;
    [SerializeField] private float maxStopTime = 3;
    [SerializeField, Range(0, 1)] private float stopChance = 0.5f;

    private List<Visitor> visitors = new List<Visitor>();

    private void Awake()
    {
        System.Array.Sort(Floors, (a, b) => b.position.y.CompareTo(a.position.y));
    }

    private void Start()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] == null) continue;
            StartCoroutine(MovePlatform(platforms[i].transform));
        }
    }

    private IEnumerator MovePlatform(Transform platform)
    {
        int currentFloorIndex = GetClosestFloorIndex(platform.position);
        platform.position = new Vector2(platform.position.x, Floors[currentFloorIndex].position.y);

        int direction = Random.value < 0.5f ? 1 : -1;

        if (currentFloorIndex == 0) direction = 1;
        else if (currentFloorIndex == Floors.Length - 1) direction = -1;

        while (true)
        {
            int nextFloorIndex = currentFloorIndex + direction;
            if (nextFloorIndex < 0 || nextFloorIndex >= Floors.Length)
            {
                direction *= -1;
                nextFloorIndex = currentFloorIndex + direction;
            }

            Transform targetFloor = Floors[nextFloorIndex];
            float targetY = targetFloor.position.y;

            while (Mathf.Abs(platform.position.y - targetY) > 0.001f)
            {
                Vector2 newPos = Vector2.MoveTowards(
                    platform.position,
                    new Vector2(platform.position.x, targetY),
                    platformSpeed * Time.deltaTime);
                platform.position = newPos;
                yield return null;
            }

            platform.position = new Vector2(platform.position.x, targetY);
            currentFloorIndex = nextFloorIndex;

            if (currentFloorIndex == 0) direction = 1;
            else if (currentFloorIndex == Floors.Length - 1) direction = -1;

            if (Random.value < stopChance)
            {
                yield return new WaitForSeconds(Random.Range(minStopTime, maxStopTime));
            }
        }
    }

    private int GetClosestFloorIndex(Vector2 position)
    {
        int closest = 0;
        float minDistance = Mathf.Abs(position.y - Floors[0].position.y);
        for (int i = 1; i < Floors.Length; i++)
        {
            float distance = Mathf.Abs(position.y - Floors[i].position.y);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = i;
            }
        }
        return closest;
    }

    public void AddVisitor(Visitor visitor)
    {
        visitors.Add(visitor);
        visitor.gameObject.SetActive(false);
        int currentFloorIndex = GetClosestFloorIndex(visitor.transform.position);
        int nextFloorIndex = currentFloorIndex + 1;
        if (nextFloorIndex >= Floors.Length)
        {
            ReleaseVisitor(visitor, Floors[currentFloorIndex].position, Vector2.left);
            return;
        }
        Vector2 direction = GetExitDirection(nextFloorIndex == Floors.Length - 1);
        Vector2 spawnPos = new Vector2(transform.position.x + direction.x * spawnOffset, Floors[nextFloorIndex].position.y);
        ReleaseVisitor(visitor, spawnPos, direction);
    }

    private void ReleaseVisitor(Visitor visitor, Vector2 spawnPos, Vector2 direction)
    {
        visitor.transform.position = spawnPos;
        visitor.SetDirectionAndResume(direction);
        visitor.gameObject.SetActive(true);
        visitors.Remove(visitor);
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