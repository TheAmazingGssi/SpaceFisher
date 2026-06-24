using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public enum DragDirection { Up, Down, Left, Right }

    [SerializeField] private DragDirection dragDirection = DragDirection.Up;
    [SerializeField] private float slideSpeed = 15;
    [SerializeField] private float snapThreshold = 0.3f;

    [SerializeField] private Vector2 closedPosition;
    [SerializeField] private Vector2 openPosition;

    [SerializeField] private RectTransform rectTransform;
    private bool isDragging = false;
    private bool isOpen = false;
    private Vector2 targetPosition;

    private void Awake()
    {
        targetPosition = closedPosition;
        rectTransform.anchoredPosition = closedPosition;
    }

    private void Update()
    {
        if (!isDragging)
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * slideSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Pointer.current == null) return;

        Vector2 delta = Pointer.current.delta.ReadValue();
        Vector2 currentPos = rectTransform.anchoredPosition;

        if (dragDirection == DragDirection.Up || dragDirection == DragDirection.Down)
        {
            currentPos.y += delta.y;
            float minY = Mathf.Min(closedPosition.y, openPosition.y);
            float maxY = Mathf.Max(closedPosition.y, openPosition.y);
            currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);
        }
        else
        {
            currentPos.x += delta.x;
            float minX = Mathf.Min(closedPosition.x, openPosition.x);
            float maxX = Mathf.Max(closedPosition.x, openPosition.x);
            currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        }

        rectTransform.anchoredPosition = currentPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        float totalDistance = Vector2.Distance(closedPosition, openPosition);
        if (totalDistance <= 0.01f) return;

        float currentDistanceToClosed = Vector2.Distance(rectTransform.anchoredPosition, closedPosition);
        float currentProgress = currentDistanceToClosed / totalDistance;

        if (isOpen)
        {
            if (currentProgress < (1f - snapThreshold)) SetState(false);
            else SetState(true);
        }
        else
        {
            if (currentProgress > snapThreshold) SetState(true);
            else SetState(false);
        }
    }

    public void SetState(bool open)
    {
        isOpen = open;
        targetPosition = open ? openPosition : closedPosition;
    }
}