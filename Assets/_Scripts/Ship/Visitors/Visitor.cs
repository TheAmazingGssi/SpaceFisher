using System;
using UnityEngine;

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
    private Vector2 moveDirection;
    private bool isMoving;
    public VisitorData Data { get; private set; }

    private Transform lastPosition;

    public void Initialize(VisitorData data, Vector2 direction)
    {
        Data = data;
        spriteRenderer.sprite = data.Sprite;
        moveDirection = direction.normalized;
        isMoving = true;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isMoving) return;
        transform.Translate(moveDirection * Data.MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.Building))
            HandleBuildingCollision(other);
    }

    private void HandleBuildingCollision(Collider2D buildingCollider)
    {
        if (!buildingCollider.TryGetComponent<Store>(out Store store)) return;
        if (UnityEngine.Random.value > Data.EnterBuildingChance) return;
        CurrentLocation = store.StoreType;
        Bus<ChangeLocation>.Raise(new ChangeLocation { Visitor = this });
        gameObject.SetActive(false);
    }
}
