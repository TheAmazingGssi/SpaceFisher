using UnityEngine;
using UnityEditor.VersionControl;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{
    public string ID { get => id; }
    [SerializeField] private string id;
    public Sprite FishSprite { get => fishSprite; }
    [SerializeField] private Sprite fishSprite;

    [Header("Minigame")]
    public float MGSpeed { get => mgSpeed; }
    [SerializeField] private float mgSpeed;
    public float WiggleAngleMax { get => wiggleAngleMax; }
    [SerializeField][Range(-90,180)] private float wiggleAngleMax = 90;
    public float WiggleAngleMin { get => wiggleAngleMin; }
    [SerializeField][Range(-90,180)] private float wiggleAngleMin = 0;
    public float WiggleSpeed { get => defaultWiggleSpeed? mgSpeed * 100 : wiggleSpeed; }
    [SerializeField] private float wiggleSpeed;
    [SerializeField] bool defaultWiggleSpeed;

    [Header("Aquarium")]
    public Vector2 SwimTime { get => swimTime; }
    [Tooltip("X: Min Y: Max")]
    [SerializeField] private Vector2 swimTime;
    public float PauseTime { get => pauseTime; }
    [SerializeField] private float pauseTime;
    public float AquariumSpeed { get => aquariumSpeed; }
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
        if (!FishTypeList.Instance.list.ContainsKey(id))
        {
            FishTypeList.Instance.list.Add(id, this);
            EditorUtility.SetDirty(FishTypeList.Instance);
        }
    }
#endif
}
