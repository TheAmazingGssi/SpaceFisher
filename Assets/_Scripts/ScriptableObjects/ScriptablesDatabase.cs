using NativeSerializableDictionary;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "ScriptablesDatabase", menuName = "Scriptable Objects/ScriptablesDatabase")]
public class ScriptablesDatabase : SingletonScriptableObject<ScriptablesDatabase>
{
    public Dictionary<string, FishStats> fishList;
    public Dictionary<string, BuildingData> storeList;
    public Dictionary<string, PlanetFishTable> planetList;
    private void OnValidate()
    {
        if(fishList == null)
        {
            fishList = new Dictionary<string, FishStats>();
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
            storeList = new Dictionary<string, BuildingData>();
        }
        storeList.Clear();
        guids = AssetDatabase.FindAssetGUIDs("t:" + nameof(BuildingData));
        foreach (GUID guid in guids)
            if (!storeList.ContainsKey(guid.ToString()))
            {
                BuildingData fishStat = AssetDatabase.LoadAssetByGUID<BuildingData>(guid);
                storeList.Add(guid.ToString(), fishStat);
            }
        if (planetList == null)
        {
            planetList = new Dictionary<string, PlanetFishTable>();
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
}
