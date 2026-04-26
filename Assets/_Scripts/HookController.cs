using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

enum MinigamePhase { PreGame, Down, Up }
public class HookController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform playerObject;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float downSpeed;
    [SerializeField] float upSpeed;
    Vector3 currentTouch;
    MinigamePhase currentPhase = MinigamePhase.Down;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!cam)
        {
            cam = Camera.main;
            Debug.LogWarning("Assign scene camera in inspector");
        }
        currentTouch = transform.position;
        FishAI.FishCaught.AddListener(OnFishCaught);
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
        switch(currentPhase)
        {
            case MinigamePhase.Down:
                return -downSpeed * Time.deltaTime;
            case MinigamePhase.Up:
                return upSpeed * Time.deltaTime;
            default:
                return 0;
        }
    }
    void OnTouch(InputValue value)
    {
        Vector2 screenSpacePos = value.Get<Vector2>();
        currentTouch = cam.ScreenToWorldPoint(screenSpacePos);
        currentTouch.z = transform.position.z;
    }

    void OnFishCaught(FishAI fish)
    {
        if (currentPhase == MinigamePhase.Down) 
            currentPhase = MinigamePhase.Up;
    }
}
