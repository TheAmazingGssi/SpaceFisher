using UnityEngine;

public class FishManager : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer SpriteRenderer {  get; private set; }
    [field: SerializeField] public FishStats FishStats {  get; private set; }

    [SerializeField] private CapsuleCollider2D fishCollider;

    public void Initialize(FishStats fishStats)
    {
        FishStats = fishStats;
        SpriteRenderer.sprite = FishStats.FishSprite;
    }


}
