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
    private SerializedProperty customSpeed;
    private SerializedProperty speed;
    private SerializedProperty planet;

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
        customSpeed = serializedObject.FindProperty("customSpeed");
        speed = serializedObject.FindProperty("speed");
        planet = serializedObject.FindProperty("planet");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("ID: " + id.stringValue);
        EditorGUILayout.PropertyField(fishSprite);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Speed", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(customSpeed, new GUIContent("Manual Speed Control"));

        if (!customSpeed.boolValue)
            EditorGUILayout.PropertyField(speed, new GUIContent("Speed"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Minigame", EditorStyles.boldLabel);

        if (customSpeed.boolValue)
            EditorGUILayout.PropertyField(minigameSpeed, new GUIContent("MG Speed"));

        EditorGUILayout.PropertyField(wiggleAngleMax);
        EditorGUILayout.PropertyField(wiggleAngleMin);
        EditorGUILayout.PropertyField(defaultWiggleSpeed);

        if (!defaultWiggleSpeed.boolValue)
            EditorGUILayout.PropertyField(wiggleSpeed);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Aquarium", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(swimTime);
        EditorGUILayout.PropertyField(pauseTime);

        if (customSpeed.boolValue)
            EditorGUILayout.PropertyField(aquariumSpeed, new GUIContent("Aquarium Speed"));

        EditorGUILayout.PropertyField(verticalSwimming);
        serializedObject.ApplyModifiedProperties();
    }
}
