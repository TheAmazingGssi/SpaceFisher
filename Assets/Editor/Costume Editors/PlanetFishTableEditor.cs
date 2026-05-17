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
        base.OnInspectorGUI();
    }

    private void DisplayFishSpawnData(int index)
    {

    }
}
