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
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            currentDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SetFrontAngle(currentDirection);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Tags.AquariumWall))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflected = Vector2.Reflect(currentDirection, normal);
            float offset = Random.Range(-45, 45) * Mathf.Deg2Rad;
            currentDirection = new Vector2(reflected.x * Mathf.Cos(offset) - reflected.y * Mathf.Sin(offset), reflected.x * Mathf.Sin(offset) + reflected.y * Mathf.Cos(offset)).normalized;
            rb.linearVelocity = currentDirection * manager.Stats.AquariumSpeed;
            SetFrontAngle(currentDirection);
        }
    }
    private void SetFrontAngle(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (direction.x < 0 ? -1 : 1),transform.localScale.y,transform.localScale.z);
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