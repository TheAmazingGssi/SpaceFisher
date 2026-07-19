using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FishAI : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [field: SerializeField] public FishStats Stats { get; private set; }
    private FishPool parentPool;

    float wiggleDirection = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Bus<MinigameEnd>.OnEvent +=OnMinigameEnd;
    }
    private void OnDisable()
    {
        Bus<MinigameEnd>.OnEvent -=OnMinigameEnd;
    }
    private void Update()
    {
        HookedMovement();
    }
    public void Setup(FishStats stats, FishPool pool)
    {
        Stats = stats;
        rb.linearVelocity = transform.right * Stats.MGSpeed;
        spriteRenderer.sprite = Stats.FishSprite;
        //spriteRenderer.transform.localPosition = new Vector3(-spriteRenderer.bounds.size.x / 2, 0, 0);
        wiggleDirection = 0;
        parentPool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.Player)
        {
            transform.SetParent(collision.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45);
            rb.linearVelocity = Vector2.zero;
            wiggleDirection = Random.Range(0, 2) == 0 ? 1 : -1;
            Bus<FishCaught>.Raise(new FishCaught { Fish = this });
        }
    }
    private void HookedMovement()
    {
        if (wiggleDirection == 0)
            return;

        float newZ = transform.rotation.eulerAngles.z + (wiggleDirection * Stats.WiggleSpeed * Time.deltaTime);
        if ((Mathf.DeltaAngle(newZ, Stats.WiggleAngleMax) < 0 && wiggleDirection > 0) || (Mathf.DeltaAngle(newZ, Stats.WiggleAngleMin) > 0 && wiggleDirection < 0))
        {
            wiggleDirection *= -1;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newZ);
    }
    public void ReturnToPool()
    {
        transform.SetParent(null);
        parentPool.Push(gameObject);
    }
    private void OnMinigameEnd(MinigameEnd e)
    {
        ReturnToPool();
    }
    public void KnockOff()
    {
        transform.parent = null;
        wiggleDirection = 0;
        rb.angularVelocity = Stats.WiggleSpeed * 2;
        Vector2 forward = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
        rb.linearVelocity = -forward * Stats.MGSpeed * 2;
    }
}
