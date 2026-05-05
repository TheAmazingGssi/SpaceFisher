using NativeSerializableDictionary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FishTypeList", menuName = "Scriptable Objects/FishTypeList")]
public class FishTypeList : SingletonScriptableObject<FishTypeList>
{
    public SerializableDictionary<string, FishStats> list;
    private void OnValidate()
    {

        FishStats[] assets = Resources.LoadAll<FishStats>("");
        foreach(var asset in assets)
            if (!list.ContainsKey(asset.ID))
            {
                list.Add(asset.ID, asset);
                EditorUtility.SetDirty(this);
            }
    }

}
