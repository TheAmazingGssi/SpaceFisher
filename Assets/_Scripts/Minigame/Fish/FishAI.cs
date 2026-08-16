using UnityEngine;

public class FishAI : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem particles;
    [field: SerializeField] public FishStats Stats { get; private set; }
    private FishPool parentPool;

    float wiggleDirection = 0;

    Transform magnetTarget;

    private enum State { Swim, Hooked, Magnetized }
    private State state;

    const string CatchAnimTrigger = "Caught";

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
        switch(state)
        {
            case State.Hooked:
                HookedMovement();
                break;
            case State.Magnetized:
                MagnetizedMovement();
                break;
            case State.Swim:
            default:
                break;
        }
    }
    public void Setup(FishStats stats, FishPool pool)
    {
        Stats = stats;
        rb.linearVelocity = transform.right * Stats.MGSpeed;
        anim.runtimeAnimatorController = Stats.Animator;
        //spriteRenderer.transform.localPosition = new Vector3(-spriteRenderer.bounds.size.x / 2, 0, 0);
        wiggleDirection = 0;
        state = State.Swim;
        parentPool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.Player && state != State.Hooked)
        {
            transform.SetParent(collision.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45);
            rb.linearVelocity = Vector2.zero;
            wiggleDirection = Random.Range(0, 2) == 0 ? 1 : -1;
            Bus<FishCaught>.Raise(new FishCaught { Fish = this });
            state = State.Hooked;
            particles.Play();
            anim.SetTrigger(CatchAnimTrigger);
        }
        if(collision.tag == Constants.Tags.Magnet && state != State.Hooked && state != State.Magnetized)
        {
            state = State.Magnetized;
            magnetTarget = collision.transform;
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
    private void MagnetizedMovement()
    {
        Vector2 dir = magnetTarget.position - transform.position;
        dir.Normalize();
        rb.linearVelocity = dir * (MinigameManager.Instance.Hook.UpSpeed * 1.3f);
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
