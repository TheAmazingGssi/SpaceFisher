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

        //==========Generic Stats==========
        EditorGUILayout.LabelField("ID: " + id.stringValue);
        EditorGUILayout.PropertyField(fishSprite);
        EditorGUILayout.PropertyField(planet);
        Texture2D image = AssetPreview.GetAssetPreview((target as FishStats).FishSprite);
        GUILayout.Box(image);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Speed", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(customSpeed, new GUIContent("Manual Speed Control"));

        if (!customSpeed.boolValue)
            EditorGUILayout.PropertyField(speed, new GUIContent("Speed"));

        //================minigame spesific============
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Minigame", EditorStyles.boldLabel);

        if (customSpeed.boolValue)
            EditorGUILayout.PropertyField(minigameSpeed, new GUIContent("MG Speed"));
        MinMaxAngles();
        EditorGUILayout.PropertyField(defaultWiggleSpeed);

        if (!defaultWiggleSpeed.boolValue)
            EditorGUILayout.PropertyField(wiggleSpeed);

        //===============Aquarium spesific==================
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Aquarium", EditorStyles.boldLabel);
        MinMaxAquariumSpeed();
        EditorGUILayout.PropertyField(pauseTime);

        if (customSpeed.boolValue)
            EditorGUILayout.PropertyField(aquariumSpeed, new GUIContent("Aquarium Speed"));

        EditorGUILayout.PropertyField(verticalSwimming);
        serializedObject.ApplyModifiedProperties();
    }

    private void MinMaxAngles()
    {
        float min = wiggleAngleMin.floatValue;
        float max = wiggleAngleMax.floatValue;

        EditorGUILayout.BeginHorizontal();
        float defaultLabelSize = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 100;
        EditorGUILayout.LabelField("Wiggle Range:");
        EditorGUILayout.LabelField("Minimum Angle: " + min.ToString("F0"));
        EditorGUILayout.LabelField("Maximum Angle: " + max.ToString("F0"));
        GUILayout.FlexibleSpace();
        EditorGUIUtility.labelWidth = defaultLabelSize;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.MinMaxSlider(ref min, ref max, -90, 180);
        wiggleAngleMin.floatValue = min;
        wiggleAngleMax.floatValue = max;
    }
    private void MinMaxAquariumSpeed()
    {
        float defaultLabelSize = EditorGUIUtility.labelWidth;
        Vector2 vect = swimTime.vector2Value;
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 11;
        EditorGUILayout.LabelField("Swim Time:");
        EditorGUILayout.LabelField("Minimum:");
        vect.x = EditorGUILayout.FloatField(vect.x);
        EditorGUILayout.LabelField("Maximum:");
        vect.y = EditorGUILayout.FloatField(vect.y);
        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = defaultLabelSize;
        swimTime.vector2Value = vect;

    }
}
