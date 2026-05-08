using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FishAI : MonoBehaviour
{
    const string playerTag = "Player"; //change to tag from constants class plz
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [field:SerializeField] public FishStats Stats { get; private set; }

    float wiggleDirection = 0;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rb.linearVelocity = transform.right * Stats.MGSpeed;
        spriteRenderer.sprite = Stats.FishSprite;
        spriteRenderer.transform.localPosition = new Vector3(-spriteRenderer.bounds.size.x/2, 0, 0);
        wiggleDirection = 0;
    }
    private void Update()
    {
        HookedMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
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
}
