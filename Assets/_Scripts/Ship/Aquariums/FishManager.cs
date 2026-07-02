using UnityEngine;

public class FishManager : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer SpriteRenderer {  get; private set; }
    [field: SerializeField] public FishStats Stats {  get; private set; }
    [field: SerializeField] public Animator animator {  get; private set; }

    [SerializeField] private CapsuleCollider2D fishCollider;

    public void Init(FishStats stats)
    {
        Stats = stats;
        SpriteRenderer.sprite = Stats.FishSprite;
        if (stats.Animator != null)
        {
            animator.enabled = true;
            animator.runtimeAnimatorController = stats.Animator;
        }
        else
            animator.enabled = false;
    }

}
