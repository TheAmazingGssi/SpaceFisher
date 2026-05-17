using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    static public PlacementManager Instance;
    [SerializeField] GridManager gridManager;
    public MoveableObject CurrentlyMovingObject;

    Vector2 currentTouch;
    Camera cam;

    public bool CanTakeObject { get => !(bool)CurrentlyMovingObject; }

    private void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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

    public void OnTouch(InputAction.CallbackContext context)
    {
        Vector2 screenSpacePos = context.ReadValue<Vector2>();
        currentTouch = cam.ScreenToWorldPoint(screenSpacePos);
    }

}
