using UnityEngine;

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{
    [SerializeField] private Sprite fishSprite;

    [Header("Minigame")]
    [SerializeField] private float mgSpeed;
    [SerializeField] private float height;

    [Header("Aquarium")]
    [Tooltip("X: Min Y: Max")]
    [SerializeField] private Vector2 swimTime;
    [SerializeField] private float pauseTime;
    [SerializeField] private float aquariumSpeed;


    public float MGSpeed => mgSpeed;
    public float Height => height;
    public Vector2 SwimTime => swimTime;
    public float PauseTime => pauseTime;
    public float AquariumSpeed => aquariumSpeed;
    public Sprite FishSprite => fishSprite;

    private void Reset()
    {
        Debug.Log("RESETTT");
    }

}
