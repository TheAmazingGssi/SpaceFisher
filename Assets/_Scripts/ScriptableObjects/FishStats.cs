using UnityEngine;
using UnityEditor.VersionControl;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{    
    [SerializeField] private string id;
    [SerializeField] private Sprite fishSprite;

    [Header("Minigame")]
    [SerializeField] private float mgSpeed;

    [Header("Aquarium")]
    [Tooltip("X: Min Y: Max")]
    [SerializeField] private Vector2 swimTime;
    [SerializeField] private float pauseTime;
    [SerializeField] private float aquariumSpeed;
    [SerializeField][Range(0, 90)] private float verticalSwimming = 30;

    public float MGSpeed => mgSpeed;
    public Vector2 SwimTime => swimTime;
    public float PauseTime => pauseTime;
    public float AquariumSpeed => aquariumSpeed;
    public float VerticalSwimming => verticalSwimming;
    public Sprite FishSprite => fishSprite;
    public string ID => id;

    


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
