using UnityEngine;

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [SerializeField] private Sprite fishSprite;

    [Header("Minigame")]
    [SerializeField] private float mgSpeed;
    //[SerializeField] private float height;

    [Header("Aquarium")]
    [Tooltip("X: Min Y: Max")]
    [SerializeField] private Vector2 swimTime;
    [SerializeField] private float pauseTime;
    [SerializeField] private float aquariumSpeed;
    [SerializeField][Range(0, 90)] private float verticalSwimming = 30;



    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = System.Guid.NewGuid().ToString();
        }
    }

    public float MGSpeed => mgSpeed;
    //public float Height => height;
    public Vector2 SwimTime => swimTime;
    public float PauseTime => pauseTime;
    public float AquariumSpeed => aquariumSpeed;
    public float VerticalSwimming => verticalSwimming;
    public Sprite FishSprite => fishSprite;
    public string ID { get; private set; } = System.Guid.NewGuid().ToString();
}
