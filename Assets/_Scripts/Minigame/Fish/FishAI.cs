using UnityEngine;
using UnityEngine.Events;

public class FishAI : MonoBehaviour
{
    const string playerTag = "Player"; //change to tag from constants class plz
    [SerializeField] Rigidbody2D rb;
    [field:SerializeField] public FishStats Stats { get; private set; }
    static public UnityEvent<FishAI> FishCaught = new UnityEvent<FishAI>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * Stats.MGSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            transform.SetParent(collision.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 0, 45);
            rb.linearVelocity = Vector2.zero;
            FishCaught.Invoke(this);
        }
    }
}
