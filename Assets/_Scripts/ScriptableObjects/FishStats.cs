using UnityEngine;
using UnityEditor.VersionControl;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{    
    [field: SerializeField] public string ID {  get; private set; }
    [field: SerializeField] public Sprite FishSprite { get; private set; }

    [field: Header("Minigame")]
    [field: SerializeField] public float MGSpeed { get; private set; }
    [field: SerializeField][Range(-90,180)] public float WiggleAngleMax { get; private set; } = 90;
    [field: SerializeField][Range(-90,180)] public float WiggleAngleMin { get; private set; } = 0;
    [field: SerializeField] public float WiggleSpeed { get; private set; }

    [field: Header("Aquarium")]
    [field: Tooltip("X: Min Y: Max")]
    [field: SerializeField] public Vector2 SwimTime { get; private set; }
    [field: SerializeField] public float PauseTime { get; private set; }
    [field: SerializeField] public float AquariumSpeed { get; private set; }
    [field: SerializeField][Range(0, 90)] public float VerticalSwimming { get; private set; } = 30;


#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);

        if (!string.IsNullOrEmpty(path))
        {
            string guid = AssetDatabase.AssetPathToGUID(path);

            if (ID != guid)
            {
                ID = guid;
                EditorUtility.SetDirty(this);
            }
        }
        if (!FishTypeList.Instance.list.ContainsKey(ID))
        {
            FishTypeList.Instance.list.Add(ID, this);
            EditorUtility.SetDirty(FishTypeList.Instance);
        }
    }
#endif
}
