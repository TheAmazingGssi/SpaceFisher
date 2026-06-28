using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Planet
{
    Lava,
    Mist,
    Other
}

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{
    public string ID { get => id; }
    [SerializeField] private string id;
    public Sprite FishSprite { get => fishSprite; }
    [SerializeField] private Sprite fishSprite;

    public Planet Planet {  get => planet; }
    [SerializeField] private Planet planet;

    public float Value { get => value; }
    [SerializeField] private float value;

    public int Price { get => price; }
    [SerializeField] private int price;
    public AnimatorOverrideController Animator => animator;
    [SerializeField] private AnimatorOverrideController animator;

    [SerializeField] private bool customSpeed;
    [SerializeField] private float speed;

    [Header("Minigame")]
    public float MGSpeed { get => customSpeed ? mgSpeed : speed; }
    [SerializeField] private float mgSpeed;
    public float WiggleAngleMax { get => wiggleAngleMax; }
    [SerializeField][Range(-90,180)] private float wiggleAngleMax = 90;
    public float WiggleAngleMin { get => wiggleAngleMin; }
    [SerializeField][Range(-90,180)] private float wiggleAngleMin = 0;
    public float WiggleSpeed { get => defaultWiggleSpeed? mgSpeed * 100 : wiggleSpeed; }
    [SerializeField] private float wiggleSpeed;
    [SerializeField] bool defaultWiggleSpeed;
    public float MaxSpawnRate { get => maxSpawnRate; }
    [SerializeField][Range(0, 1)] float maxSpawnRate;

    [Header("Aquarium")]
    public Vector2 SwimTime { get => swimTime; }
    [Tooltip("X: Min Y: Max")]
    [SerializeField] private Vector2 swimTime;
    public float PauseTime { get => pauseTime; }
    [SerializeField] private float pauseTime;
    public float AquariumSpeed { get => customSpeed ? aquariumSpeed : speed / 2; }
    [SerializeField] private float aquariumSpeed;
    public float VerticalSwimming { get => verticalSwimming; }
    [SerializeField][Range(0, 90)] private float verticalSwimming = 30;


#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);

        if (!string.IsNullOrEmpty(path))
        {
            string guid = AssetDatabase.AssetPathToGUID(path);

            if (id != guid)
            {
                id = guid;
                EditorUtility.SetDirty(this);
            }
        }
    //    if (ScriptablesDatabase.Instance.fishList == null)
    //    {
    //        ScriptablesDatabase.Instance.fishList = new System.Collections.Generic.Dictionary<string, FishStats>();
    //    }
    //    if (!ScriptablesDatabase.Instance.fishList.ContainsKey(id))
    //    {
    //        ScriptablesDatabase.Instance.fishList.Add(id, this);
    //        EditorUtility.SetDirty(ScriptablesDatabase.Instance);
    //    }
    }
#endif
}
