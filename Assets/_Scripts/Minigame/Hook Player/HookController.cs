using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

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
    public float Speed { get
        {
            switch(MinigameManager.Instance.Phase)
            {
                case MinigamePhase.Down:
                    return downSpeed;
                case MinigamePhase.Up:
                    return upSpeed;
                default:
                    return 0;
            }
        }
    }
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
        Bus<FishCaught>.OnEvent += OnFishCaught;
    }
    private void OnDisable()
    {
        Bus<FishCaught>.OnEvent -= OnFishCaught;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, MoveUpDown()));
        playerObject.Translate(new Vector2(MoveLeftRight(), 0));
        fishingLine.SetPosition(1, playerObject.position);
        if(transform.position.y > 0 && MinigameManager.Instance.Phase == MinigamePhase.Up)
        {
            transform.position = new Vector2(transform.position.x, 0);
            Bus<MinigameEnd>.Raise(new MinigameEnd());
        }
    }
    #endregion
    #region Movement
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
    #endregion
    #region InputEvents
    void OnTouch(InputValue value)
    {
        Vector2 screenSpacePos = value.Get<Vector2>();
        currentTouch = cam.ScreenToWorldPoint(screenSpacePos);
        currentTouch.z = transform.position.z;
    }
    void OnTap(InputValue value)
    {
        Vector2 tapPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        if (MinigameManager.Instance.Phase == MinigamePhase.PreGame && !IsOverUI(tapPosition))
            Bus<MinigameStart>.Raise(new MinigameStart());
    }
    #endregion
    bool IsOverUI(Vector2 screenPos)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);

        pointerData.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }
    void OnFishCaught(FishCaught e)
    {
        if (MinigameManager.Instance.Phase == MinigamePhase.Down)
            MinigameManager.Instance.Phase = MinigamePhase.Up;
    }
}
