using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string ID { get => id; }
    [SerializeField] private string id;
    public Location StoreType => storeType;
    [SerializeField] private Location storeType;
    public Sprite Sprite => sprite; //TODO: hide if aquarium
    [SerializeField] private Sprite sprite;
    public int Price => price;
    [SerializeField] private int price = 10;
    public int Value => value;
    [SerializeField] private int value = 10;
    public float MinInterval => minInterval;
    [SerializeField] private float minInterval = 5;
    public float MaxInterval => maxInterval;
    [SerializeField] private float maxInterval = 15;

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
