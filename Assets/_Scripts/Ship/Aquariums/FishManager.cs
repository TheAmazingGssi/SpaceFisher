using UnityEngine;

public class FishManager : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer SpriteRenderer {  get; private set; }
    [field: SerializeField] public FishStats Stats {  get; private set; }

    [SerializeField] private CapsuleCollider2D fishCollider;

    public void Initialize(FishStats stats)
    {
        Stats = stats;
        SpriteRenderer.sprite = Stats.FishSprite;
    }

}
