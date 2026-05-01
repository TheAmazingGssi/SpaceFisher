using UnityEngine;

public class FishManager : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer SpriteRenderer {  get; private set; }
    [SerializeField] private CapsuleCollider2D fishCollider;
}
