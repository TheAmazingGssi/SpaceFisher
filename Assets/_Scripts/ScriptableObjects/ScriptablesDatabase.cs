using NativeSerializableDictionary;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "ScriptablesDatabase", menuName = "Scriptable Objects/ScriptablesDatabase")]
public class ScriptablesDatabase : SingletonScriptableObject<ScriptablesDatabase>
{
    public Dictionary<string, FishStats> fishList;
    public Dictionary<string, StoreData> storeList;
    private void OnValidate()
    {
        if(fishList == null)
        {
            fishList = new Dictionary<string, FishStats>();
        }
        fishList.Clear();
        GUID[] guids = AssetDatabase.FindAssetGUIDs("t:FishStats");
        foreach (GUID guid in guids)
            if (!fishList.ContainsKey(guid.ToString()))
            {
                FishStats fishStat = AssetDatabase.LoadAssetByGUID<FishStats>(guid);
                fishList.Add(guid.ToString(), fishStat);
            }
        
        if(storeList == null)
        {
            storeList = new Dictionary<string, StoreData>();
        }
        storeList.Clear();
        guids = AssetDatabase.FindAssetGUIDs("t:StoreData");
        foreach (GUID guid in guids)
            if (!storeList.ContainsKey(guid.ToString()))
            {
                StoreData fishStat = AssetDatabase.LoadAssetByGUID<StoreData>(guid);
                storeList.Add(guid.ToString(), fishStat);
            }


        //Debug.Log(storeList.Count);
    }
}
