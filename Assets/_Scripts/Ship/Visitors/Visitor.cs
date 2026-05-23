using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum Location
{
    Aquarium,
    Restaurant,
    Theater,
    GiftShop,
    Inbetween
}


public class Visitor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Location CurrentLocation { get; private set; } = Location.Inbetween;
    public VisitorData Data { get; private set; }

    private Vector2 moveDirection;
    private bool isMoving;

    private bool isEntering;
    private Vector2 enterTarget;
    private Building currentBuilding;

    public void Initialize(VisitorData data, Vector2 direction)
    {
        Data = data;
        spriteRenderer.sprite = data.Sprite;
        moveDirection = direction.normalized;
        isMoving = true;
        isEntering = false;
    }

    public void ResumeMovement()
    {
        CurrentLocation = Location.Inbetween;
        isMoving = true;
    }

    private void Update()
    {
        if (isEntering)
        {
            WalkToCenter();
            return;
        }
        if (isMoving)
            transform.Translate(moveDirection * Data.MoveSpeed * Time.deltaTime);
    }

    private void WalkToCenter()
    {
        transform.position = Vector2.MoveTowards(transform.position, enterTarget, Data.MoveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, enterTarget) < 0.01f)
            FinishEntering();
    }

    private void FinishEntering()
    {
        isEntering = false;
        CurrentLocation = currentBuilding.BuildingType;
        Bus<ChangeLocation>.Raise(new ChangeLocation { Visitor = this, Building = currentBuilding });
        if (CurrentLocation != Location.Aquarium)
            gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntering) return;
        if (other.CompareTag(Constants.Tags.Building))
            HandleBuildingCollision(other);
    }

    private void HandleBuildingCollision(Collider2D buildingCollider)
    {
        if (!buildingCollider.TryGetComponent<Building>(out Building building)) return;
        if (building == currentBuilding) return;
        if (UnityEngine.Random.value > Data.EnterBuildingChance) return;

        isMoving = false;
        isEntering = true;
        currentBuilding = building;
        enterTarget = building.GetEntryPoint(buildingCollider, transform.position);
    }
}
