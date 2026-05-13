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
        DrawAnglePreview(-verticalSwimming.floatValue, verticalSwimming.floatValue, false);
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

        DrawAnglePreview(min, max, true);

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

    void DrawAnglePreview(float minAngle, float maxAngle, bool flip)
    {
        Rect rect = GUILayoutUtility.GetRect(200, 200);

        Vector2 center = rect.center;
        float radius = 50f;

        Handles.BeginGUI();

        // Draw background circle
        Handles.color = Color.gray;
        Handles.DrawWireDisc(center, Vector3.forward, radius);

        // Convert angles to directions
        Vector3 startDir = AngleToUnitVect(minAngle) * (flip? -1 : 1);
        Vector3 endDir = AngleToUnitVect(maxAngle) * (flip ? -1 : 1);

        // Draw border lines
        Handles.color = Color.white;
        Handles.DrawLine(center, center + (Vector2)(startDir * radius));
        Handles.DrawLine(center, center + (Vector2)(endDir * radius));

        Handles.EndGUI();
    }

    Vector3 AngleToUnitVect(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), -Mathf.Sin(rad), 0);
    }
}

