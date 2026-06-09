using NativeSerializableDictionary;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "ScriptablesDatabase", menuName = "Scriptable Objects/ScriptablesDatabase")]
public class ScriptablesDatabase : SingletonScriptableObject<ScriptablesDatabase>
{
    [SerializeField] public SerializableDictionary<string, FishStats> fishList;
    [SerializeField] public SerializableDictionary<string, StoreData> storeList;
    [SerializeField] public SerializableDictionary<string, PlanetFishTable> planetList;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(fishList == null)
        {
            fishList = new SerializableDictionary<string, FishStats>();
        }
        fishList.Clear();
        GUID[] guids = AssetDatabase.FindAssetGUIDs("t:" + nameof(FishStats));
        foreach (GUID guid in guids)
            if (!fishList.ContainsKey(guid.ToString()))
            {
                FishStats fishStat = AssetDatabase.LoadAssetByGUID<FishStats>(guid);
                fishList.Add(guid.ToString(), fishStat);
            }
        
        if(storeList == null)
        {
            storeList = new SerializableDictionary<string, StoreData>();
        }
        storeList.Clear();
        guids = AssetDatabase.FindAssetGUIDs("t:" + nameof(BuildingData));
        foreach (GUID guid in guids)
            if (!storeList.ContainsKey(guid.ToString()))
            {
                StoreData storeStat = AssetDatabase.LoadAssetByGUID<StoreData>(guid);
                storeList.Add(guid.ToString(), storeStat);
            }
        if (planetList == null)
        {
            planetList = new SerializableDictionary<string, PlanetFishTable>();
        }
        planetList.Clear();
        guids = AssetDatabase.FindAssetGUIDs("t:" + nameof(PlanetFishTable));
        foreach (GUID guid in guids)
            if (!planetList.ContainsKey(guid.ToString()))
            {
                PlanetFishTable fishStat = AssetDatabase.LoadAssetByGUID<PlanetFishTable>(guid);
                planetList.Add(guid.ToString(), fishStat);
            }
    }
#endif
}
