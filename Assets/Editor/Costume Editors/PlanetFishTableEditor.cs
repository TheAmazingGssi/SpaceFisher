using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(PlanetFishTable))]
public class PlanetFishTableEditor : Editor
{
    private SerializedProperty planet;
    private SerializedProperty maxDepth;
    private SerializedProperty fishSpawnData;

    private void OnEnable()
    {
        planet = serializedObject.FindProperty("planet");
        maxDepth = serializedObject.FindProperty("maxDepth");
        fishSpawnData = serializedObject.FindProperty("fishSpawnData");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); //Most of the code appears on FishSpawnDataDrawer
        if (GUILayout.Button("Auto Import Fish"))
            AutoImportFish();
    }
    private void AutoImportFish()
    {
        if (!EditorUtility.DisplayDialog("Are You Sure About That", "This action will create a new array with all the fish that signed \"" + (target as PlanetFishTable).Planet + "\" as their planet, with default values for everything else.", "I know what Im doing", "Wait I dont want the current values to be erased"))
            return;
        //Load assets using list
        GUID[] guids = AssetDatabase.FindAssetGUIDs("t:FishStats");
        List<FishStats> listFish = new List<FishStats>();
        foreach (GUID guid in guids)
        {
            FishStats fishStat = AssetDatabase.LoadAssetByGUID<FishStats>(guid);
            if (fishStat && fishStat.Planet == (target as PlanetFishTable).Planet)
                listFish.Add(fishStat);

        }
        float maxHight = (target as PlanetFishTable).MaxDepth;
        float peakHeight = maxHight / 2f;

        fishSpawnData.ClearArray();
        fishSpawnData.arraySize = listFish.Count;
        
        for(int i = 0; i < listFish.Count; i++)
        {
            SerializedProperty element = fishSpawnData.GetArrayElementAtIndex(i);
            element.FindPropertyRelative("fishType").objectReferenceValue = listFish[i];
            element.FindPropertyRelative("minHeight").floatValue = 0;
            element.FindPropertyRelative("populationPeak").floatValue = peakHeight;
            element.FindPropertyRelative("maxHeight").floatValue = maxHight;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
