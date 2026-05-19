using UnityEditor;
using UnityEditor.TerrainTools;
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
        if (!EditorUtility.DisplayDialog("Are You Sure About That", "This action will create a new array with all the fish that signed " + (target as PlanetFishTable).Planet + " as their planet, with default values for everything else.", "I know what Im doing", "Wait I dont want the current values to be erased"))
            return;
        

    }
}
