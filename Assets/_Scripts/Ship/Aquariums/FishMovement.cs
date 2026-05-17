using UnityEngine;
using System.Collections;
public class FishMovement : MonoBehaviour
{
    [SerializeField] private FishManager manager;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 currentDirection;
    private Coroutine movementCoroutine;
    private void OnEnable()
    {
        StartMoving();
    }
    private void StartMoving()
    {
        movementCoroutine = StartCoroutine(Movement());
    }
    private IEnumerator Movement()
    {
        while (true)
        {
            currentDirection = GetRandomDirection();
            SetAngle(currentDirection);
            float swimTime = Random.Range(manager.Stats.SwimTime.x, manager.Stats.SwimTime.y);
            float elapsed = 0;
            while (elapsed < swimTime)
            {
                rb.linearVelocity = currentDirection * manager.Stats.AquariumSpeed;
                elapsed += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(manager.Stats.PauseTime);
        }
    }
    private Vector2 GetRandomDirection()
    {
        float horizontalAngle = Random.value < 0.5f ? 0f : 180f;
        float verticalAngle = Random.Range(-manager.Stats.VerticalSwimming, manager.Stats.VerticalSwimming);
        float totalAngle = (horizontalAngle + verticalAngle) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(totalAngle), Mathf.Sin(totalAngle)).normalized;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Tags.AquariumWall))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflected = Vector2.Reflect(currentDirection, normal);
            float reflectedAngle = Mathf.Atan2(reflected.y, reflected.x) * Mathf.Rad2Deg;
            float clampedAngle = Mathf.Clamp(reflectedAngle, -manager.Stats.VerticalSwimming, manager.Stats.VerticalSwimming);
            if (reflected.x < 0) clampedAngle = 180f - clampedAngle;
            float offset = Random.Range(-15f, 15f);
            float finalAngle = (clampedAngle + offset) * Mathf.Deg2Rad;
            currentDirection = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle)).normalized;
            rb.linearVelocity = currentDirection * manager.Stats.AquariumSpeed;
            SetAngle(currentDirection);
        }
    }
    private void SetAngle(Vector2 direction)
    {
        Vector2 localDirection = new Vector2(Mathf.Abs(direction.x), direction.x < 0 ? -direction.y : direction.y);
        float tilt = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, tilt);
        float xScale = Mathf.Abs(transform.localScale.x) * (direction.x < 0 ? -1 : 1);
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }
    private void OnDisable()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }
}