using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FishStats))]
public class FishStatsDrawer : PropertyDrawer
{
    private const float PreviewSize = 64f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property, label);

        FishStats fishStats = property.objectReferenceValue as FishStats;
        if (fishStats)
        {
            Texture2D preview = AssetPreview.GetAssetPreview(fishStats.FishSprite);
            if (preview)
            {
                Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, PreviewSize, PreviewSize);
                GUI.DrawTexture(previewRect, preview, ScaleMode.ScaleToFit);
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + PreviewSize + 2;
    }
}