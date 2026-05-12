using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Planet))]
public class PlanetDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        Planet planet = (Planet)property.enumValueIndex;
        Color defaultColor = GUI.color;
        GUI.color = GetPlanetColor(planet);
        property.enumValueIndex = EditorGUI.Popup(position, label.text, property.enumValueIndex, property.enumDisplayNames);
        GUI.color = defaultColor;
        EditorGUI.EndProperty();
    }

    private Color GetPlanetColor(Planet planet)
    {
       switch(planet)
        {
            case Planet.Lava:
                return Color.orange;
            case Planet.Mist:
                return Color.paleGreen;
            case Planet.Other:
                return GUI.color;
            default:
                return GUI.color;
        }
    }
}
