using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string ID { get => id; }
    [SerializeField] protected string id;
    public Location StoreType => storeType;
    [SerializeField] protected Location storeType;
    public float MinInterval => minInterval;
    [SerializeField] protected float minInterval = 5;
    public float MaxInterval => maxInterval;
    [SerializeField] protected float maxInterval = 15;

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        if (!string.IsNullOrEmpty(path))
        {
            string guid = UnityEditor.AssetDatabase.AssetPathToGUID(path);
            if (id != guid)
            {
                id = guid;
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
    }
#endif
}
