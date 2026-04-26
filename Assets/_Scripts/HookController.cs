using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class HookController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform playerObject;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float downSpeed;
    [SerializeField] float upSpeed;
    Vector3 currentTouch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!cam)
        {
            cam = Camera.main;
            Debug.LogWarning("Assign scene camera in inspector");
        }
        currentTouch = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, MoveUpDown()));
        playerObject.Translate(new Vector2(MoveLeftRight(), 0));
    }
    

    float MoveLeftRight()
    {
        float delta = currentTouch.x - playerObject.position.x;
        float fullSpeed = horizontalSpeed * Mathf.Sign(delta) * Time.deltaTime;

        if (Mathf.Abs(delta) < Mathf.Abs(fullSpeed))
            return delta;
        else
            return fullSpeed;
    }
    float MoveUpDown()
    {
        return -downSpeed * Time.deltaTime;
    }
    void OnTouch(InputValue value)
    {
        Vector2 screenSpacePos = value.Get<Vector2>();
        currentTouch = cam.ScreenToWorldPoint(screenSpacePos);
        currentTouch.z = transform.position.z;
    }
}
