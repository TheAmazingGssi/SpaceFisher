using UnityEngine;
using System.Collections.Generic;

public class MoveableObject : ClickableObject
{
    [field: SerializeField] public GameObject MainParent {  get; private set; }
    [SerializeField] GameObject greenTint;
    [SerializeField] GameObject redTint;
    public bool IsMoving { get => PlacementManager.Instance.CurrentlyMovingObject == this; }


    bool canPlace;

    public void StartMoving()
    {
        InitCanPlace();
        PlacementManager.Instance.CurrentlyMovingObject = this;
        SetColor();
    }
    public bool TryStopMoving()
    {
        InitCanPlace();
        if (canPlace)
        {
            greenTint.SetActive(false);
            redTint.SetActive(false);
            PlacementManager.Instance.CurrentlyMovingObject = null;
        }
        return canPlace;
    }

    public void OnPositionUpdated()
    {
        Physics2D.SyncTransforms();
        if (canPlace && IsOverlappingAnotherBuilding())
        {
            canPlace = false;
            SetColor();
        }
        else if (!canPlace && !IsOverlappingAnotherBuilding())
        {
            canPlace = true;
            SetColor();
        }
    }
    private void InitCanPlace()
    {
        List<Collider2D> overlaps = new List<Collider2D>();
        _collider.Overlap(overlaps);
        if (overlaps.Count > 0)
            if(IsOverlappingAnotherBuilding())
            {
                canPlace = false;
                return;
            }

        canPlace = true;
    }
    private void SetColor()
    {
        if (canPlace)
        {
            greenTint.SetActive(true);
            redTint.SetActive(false);
        }
        else
        {
            greenTint.SetActive(false);
            redTint.SetActive(true);
        }
    }
    private bool IsOverlappingAnotherBuilding()
    {
        List<Collider2D> overlaps = new List<Collider2D>();
        _collider.Overlap(overlaps);
        if (overlaps.Count > 0)
            foreach (Collider2D otherCol in overlaps)
                if (otherCol.CompareTag(Constants.Tags.Building))
                    return true;

        return false;
    }

    protected override void OnFingerHold()
    {
        base.OnFingerHold();
        if (PlacementManager.Instance.CanTakeObject)
        {
            StartMoving();
        }
    }
    protected override void OnFingerUp()
    {
        TryStopMoving();
    }
}
