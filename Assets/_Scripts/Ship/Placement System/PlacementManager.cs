using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    static public PlacementManager Instance;
    [SerializeField] GridManager gridManager;
    [HideInInspector] public MoveableObject CurrentlyMovingObject;

    Vector2 currentTouch;
    [SerializeField] Camera cam;
    bool posUpdateFlag;

    public bool CanTakeObject { get => !(bool)CurrentlyMovingObject; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //cam = Camera.main;
    }

    private void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (activeTouches.Count > 0)
            currentTouch = cam.ScreenToWorldPoint(activeTouches[0].screenPosition);

        if (CurrentlyMovingObject)
        {
            Vector3 ogPos = CurrentlyMovingObject.transform.position;
            CurrentlyMovingObject.MainParent.transform.position = gridManager.AlignToGrid(currentTouch);
            if (CurrentlyMovingObject.transform.position != ogPos)
                CurrentlyMovingObject.OnPositionUpdated();
        }
    }

    private void OnValidate()
    {
        cam = Camera.main;
    }
}
