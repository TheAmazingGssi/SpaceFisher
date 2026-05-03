using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class HookController : MonoBehaviour
{
    Camera cam;
    [SerializeField] Transform playerObject;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float downSpeed;
    [SerializeField] float upSpeed;
    Vector3 currentTouch;
    //MinigamePhase currentPhase = MinigamePhase.Down;

    [SerializeField] LineRenderer fishingLine;

    public float DeltaY { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Monobehaviour
    private void OnValidate()
    {
        cam = Camera.main;
    }
    void Start()
    {
        currentTouch = playerObject.position;
    }
    private void OnEnable()
    {
        FishAI.FishCaught.AddListener(OnFishCaught);
    }
    private void OnDisable()
    {
        FishAI.FishCaught.RemoveListener(OnFishCaught);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, MoveUpDown()));
        playerObject.Translate(new Vector2(MoveLeftRight(), 0));
        fishingLine.SetPosition(1, playerObject.position);
    }
    #endregion

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
        switch(MinigameManager.Instance.Phase)
        {
            case MinigamePhase.Down:
                DeltaY = downSpeed * Time.deltaTime;
                return - DeltaY;
            case MinigamePhase.Up:
                DeltaY = upSpeed * Time.deltaTime;
                return DeltaY;
            default:
                DeltaY = 0;
                return DeltaY;
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
        if (MinigameManager.Instance.Phase == MinigamePhase.Down)
            MinigameManager.Instance.Phase = MinigamePhase.Up;
    }
}
