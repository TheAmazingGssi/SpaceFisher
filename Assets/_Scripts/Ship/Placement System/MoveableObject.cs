using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [field: SerializeField] public GameObject gameObject {  get; private set; }

    [SerializeField] BoxCollider2D collider;
    [SerializeField] SpriteRenderer tintRenderer;

    public void StartMoving()
    {

    }
}
