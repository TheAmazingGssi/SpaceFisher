using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    static public PlacementManager Instance;
    [SerializeField] GridManager gridManager;
    public MoveableObject CurrentlyMovingObject;

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
        if (CurrentlyMovingObject)
        {
            Vector3 ogPos = CurrentlyMovingObject.transform.position;
            CurrentlyMovingObject.MainParent.transform.position = gridManager.AlignToGrid(currentTouch);

            if(CurrentlyMovingObject.transform.position != ogPos )
                CurrentlyMovingObject.OnPositionUpdated();
        }
    }

    private void OnValidate()
    {
        cam = Camera.main;
    }

    void OnTouch(InputValue value)
    {
        Vector2 screenSpacePos = value.Get<Vector2>();
        currentTouch = cam.ScreenToWorldPoint(screenSpacePos);
    }

}
