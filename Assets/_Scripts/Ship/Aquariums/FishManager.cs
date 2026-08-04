using UnityEngine;
using UnityEngine.SceneManagement;

public class FishManager : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer SpriteRenderer {  get; private set; }
    [field: SerializeField] public FishStats Stats {  get; private set; }
    [field: SerializeField] public Animator animator {  get; private set; }

    [SerializeField] private CapsuleCollider2D fishCollider;

    [Header("Presentation")]
    [SerializeField] private Aquarium aquarium;
    [SerializeField] private Collider2D aquariumCollider;


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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == Constants.Scenes.PresentationShip && RunManager.Instance.FirstRun)
        {
            Bus<PlaceFish>.Raise(new PlaceFish { Fish = Stats, Amount = -1, Aquarium = aquarium });
        }
    }
}
