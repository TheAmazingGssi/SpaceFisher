using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(FishSpawnData))]
public class FishSpawnDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        PlanetFishTable tableObject = property.serializedObject.targetObject as PlanetFishTable;

        if (tableObject)
            OnGUIPlanetTable(position, property, label, tableObject);
        else
            OnGUIDefault(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        PlanetFishTable tableObject = property.serializedObject.targetObject as PlanetFishTable;

        if (tableObject)
            return (lineHeight + spacing) * 4 + FishStatsDrawer.GetPropertyHeight();
        else
            return (lineHeight + spacing) * 3 + FishStatsDrawer.GetPropertyHeight();
    }

    private void OnGUIDefault(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        Rect rect = new Rect(
            position.x,
            position.y,
            position.width,
            lineHeight
        );

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("fishType"));
        rect.y += FishStatsDrawer.GetPropertyHeight();

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("minHeight"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("populationPeak"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("maxHeight"));

        EditorGUI.EndProperty();
    }
    private void OnGUIPlanetTable(Rect position, SerializedProperty property, GUIContent label, PlanetFishTable tableObject)
    {
        EditorGUI.BeginProperty(position, label, property);
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        Rect rect = new Rect(
            position.x,
            position.y,
            position.width,
            lineHeight
        );

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("fishType"));
        rect.y += FishStatsDrawer.GetPropertyHeight();
        
        EditorGUI.LabelField(rect, "Spawn Heights:");
        rect.y += lineHeight + spacing;

        MinMaxSpawnSlider(rect, property, tableObject);
        rect.y += lineHeight + spacing;

        EditorGUI.LabelField(rect, "Depth where the spawn rate peaks:");
        rect.y += lineHeight + spacing;

        PeakSlider(rect, property, tableObject);

        EditorGUI.EndProperty();
    }
    const float numberWidth = 45f;
    const float horSpacing = 5f;
    void MinMaxSpawnSlider(Rect rect, SerializedProperty property, PlanetFishTable tableObject)
    {
        // Current values
        SerializedProperty minProp = property.FindPropertyRelative("minHeight");
        SerializedProperty maxProp = property.FindPropertyRelative("maxHeight");
        float min = minProp.floatValue;
        float max = maxProp.floatValue;

        // Layout widths


        Rect minRect = new Rect(
            rect.x,
            rect.y,
            numberWidth,
            rect.height);

        Rect sliderRect = new Rect(
            minRect.xMax + horSpacing,
            rect.y,
            rect.width - numberWidth * 2 - horSpacing * 2,
            rect.height);

        Rect maxRect = new Rect(
            sliderRect.xMax + horSpacing,
            rect.y,
            numberWidth,
            rect.height);

        // Number fields
        min = EditorGUI.FloatField(minRect, min);

        // MinMax slider
        EditorGUI.MinMaxSlider(
            sliderRect,
            ref min,
            ref max,
            0,
            tableObject.MaxDepth);

        // Number field
        max = EditorGUI.FloatField(maxRect, max);

        // Clamp values
        min = Mathf.Clamp(min, 0, tableObject.MaxDepth);
        max = Mathf.Clamp(max, min, tableObject.MaxDepth);

        // Save back
        minProp.floatValue = min;
        maxProp.floatValue = max;
    }
    void PeakSlider(Rect rect, SerializedProperty property, PlanetFishTable tableObject)
    {
        SerializedProperty peakProp = property.FindPropertyRelative("populationPeak");
        float peakVal = peakProp.floatValue;
        
        Rect minRect = new Rect(
            rect.x,
            rect.y,
            numberWidth,
            rect.height);

        Rect sliderRect = new Rect(
            minRect.xMax + horSpacing,
            rect.y,
            rect.width - numberWidth * 2 - horSpacing * 2,
            rect.height);

        peakVal = EditorGUI.FloatField(minRect, peakVal);
        peakVal = GUI.HorizontalSlider(sliderRect, peakVal, 0, tableObject.MaxDepth);

        float min = property.FindPropertyRelative("minHeight").floatValue;
        float max = property.FindPropertyRelative("maxHeight").floatValue;
        peakVal = Mathf.Clamp(peakVal, min, max);
        peakProp.floatValue = peakVal;
    }
}
