using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    public GameObject CurrentlyMovingObject;

    Vector2 currentTouch;
    Camera cam;

    private void Update()
    {
        if (CurrentlyMovingObject)
            CurrentlyMovingObject.transform.position = gridManager.AlignToGrid(currentTouch);
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
    
    public void OnTap(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() == 0)
            CurrentlyMovingObject = null;
    }
}
