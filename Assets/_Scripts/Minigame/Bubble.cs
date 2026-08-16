using UnityEngine;

public class Bubble : MonoBehaviour
{
    Vector2 mainPosition;
    [SerializeField] Vector2 wiggleModifierMaxMin;
    [SerializeField] Vector2 wiggleSpeedMaxMin;
    [SerializeField] Vector2 scaleMaxMin;
    [SerializeField] Vector2 verticalSpeedMaxMin;
    [SerializeField] Sprite[] sprites;

    [SerializeField] Camera cam;
    [SerializeField] SpriteRenderer spriteRenderer;
    const float buffer = 1;
    float currentSpeed;
    float wiggleModifier;
    float wiggleSpeed;

    private void OnEnable()
    {
        Bus<MinigameStart>.OnEvent += OnMinigameStart;
        Bus<MinigameEnd>.OnEvent += OnMinigameEnd;
    }
    private void OnDisable()
    {
        Bus<MinigameStart>.OnEvent -= OnMinigameStart;
        Bus<MinigameEnd>.OnEvent -= OnMinigameEnd;
    }
    
    private void Start()
    {
        mainPosition = transform.position;
        if(!cam)
            cam = Camera.main;
    }
    private void Update()
    {
        mainPosition.y += currentSpeed * Time.deltaTime;
        transform.position = new Vector2(mainPosition.x + (wiggleModifier * Mathf.Sin(Time.time * wiggleSpeed)), mainPosition.y);
    }

    private void OnBecameInvisible()
    {
        Teleport();
    }
    private void OnMinigameStart(MinigameStart e)
    {
        MinigameManager.Instance.Phase = MinigamePhase.Down;
        spriteRenderer.enabled = true;
        Teleport();
    }
    private void OnMinigameEnd(MinigameEnd e)
    {
        spriteRenderer.enabled = false;
    }
    public void Teleport()
    {
        Vector2 checkVect;
        float addSign;
        if (MinigameManager.Instance.Phase == MinigamePhase.Down)
        {
            checkVect = Vector2.zero;
            addSign = -buffer;
        }
        else
        {
            checkVect = Vector2.one;
            addSign = buffer;
        }

        Vector2 camEdge = cam.ViewportToWorldPoint(checkVect);
        float newY = camEdge.y + addSign;
        if (newY >= -20)
        {
            newY = -20;
        }

        camEdge.x *= Mathf.Sign(camEdge.x);
        float newX = Random.Range(-camEdge.x, camEdge.x);
        mainPosition = new Vector2(newX, newY);

        float scale = Random.Range(scaleMaxMin.x, scaleMaxMin.y);
        transform.localScale = new Vector3(scale, scale, 1);

        int index = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[index];

        currentSpeed = Random.Range(verticalSpeedMaxMin.x, verticalSpeedMaxMin.y);
        wiggleModifier = Random.Range(wiggleModifierMaxMin.x, wiggleModifierMaxMin.y);
        wiggleSpeed = Random.Range(wiggleSpeedMaxMin.x, wiggleSpeedMaxMin.y);

    }
}
