using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(FishSpawnData))]
public class FishSpawnDataEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
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

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        return (lineHeight + spacing) * 3 + FishStatsDrawer.GetPropertyHeight();
    }
}
