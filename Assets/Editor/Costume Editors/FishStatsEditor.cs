using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishStats))]
public class FishStatsEditor : Editor
{
    private SerializedProperty id;
    private SerializedProperty fishSprite;
    private SerializedProperty minigameSpeed;
    private SerializedProperty wiggleAngleMax;
    private SerializedProperty wiggleAngleMin;
    private SerializedProperty wiggleSpeed;
    private SerializedProperty defaultWiggleSpeed;
    private SerializedProperty swimTime;
    private SerializedProperty pauseTime;
    private SerializedProperty aquariumSpeed;
    private SerializedProperty verticalSwimming;

    private void OnEnable()
    {
        id = serializedObject.FindProperty("id");
        fishSprite = serializedObject.FindProperty("fishSprite");
        minigameSpeed = serializedObject.FindProperty("mgSpeed");
        wiggleAngleMax = serializedObject.FindProperty("wiggleAngleMax");
        wiggleAngleMin = serializedObject.FindProperty("wiggleAngleMin");
        wiggleSpeed = serializedObject.FindProperty("wiggleSpeed");
        defaultWiggleSpeed = serializedObject.FindProperty("defaultWiggleSpeed");
        swimTime = serializedObject.FindProperty("swimTime");
        pauseTime = serializedObject.FindProperty("pauseTime");
        aquariumSpeed = serializedObject.FindProperty("aquariumSpeed");
        verticalSwimming = serializedObject.FindProperty("verticalSwimming");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("ID: " + id.stringValue);
        EditorGUILayout.PropertyField(fishSprite);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Minigame", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(minigameSpeed);
        EditorGUILayout.PropertyField(wiggleAngleMax);
        EditorGUILayout.PropertyField(wiggleAngleMin);
        EditorGUILayout.PropertyField(defaultWiggleSpeed);
        if(!defaultWiggleSpeed.boolValue)
            EditorGUILayout.PropertyField(wiggleSpeed);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Aquarium", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(swimTime);
        EditorGUILayout.PropertyField(pauseTime);
        EditorGUILayout.PropertyField(aquariumSpeed);
        EditorGUILayout.PropertyField(verticalSwimming);

        serializedObject.ApplyModifiedProperties();
    }
}
