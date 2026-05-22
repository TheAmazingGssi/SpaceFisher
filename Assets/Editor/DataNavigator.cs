using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataNavigator : EditorWindow
{
    [MenuItem("Tools/Open Data Navigator")]
    private static void OpenWindow()
    {
        DataNavigator window = GetWindow<DataNavigator>("Navigator");
    }
    enum Tab { Fish, Buildings, Planets}

    Tab currentTab;

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Fish"))
            currentTab = Tab.Fish;
        if(GUILayout.Button("Buildings"))
            currentTab = Tab.Buildings;
        //if(GUILayout.Button("Planets"))
        //    currentTab = Tab.Planets;
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        switch(currentTab)
        {
            case Tab.Fish:
                FishGUI();
                break;
            case Tab.Buildings:
                BuildingsGUI();
                break;
            case Tab.Planets:
                GUILayout.Label("Planets");
                break;
            default:
                break;
        }
    }

    private void FishGUI()
    {
        FishStats[] fishArr = ScriptablesDatabase.Instance.fishList.Values.ToArray();
        foreach(FishStats fish in fishArr)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(fish.FishSprite.texture, GUILayout.Width(60), GUILayout.Height(60));
            GUILayout.Label(fish.name);
            if (GUILayout.Button("Edit", GUILayout.Width(60)))
            {
                Selection.activeObject = fish;
                EditorGUIUtility.PingObject(fish);
            }
            GUILayout.EndHorizontal();
        }
    }
    private void BuildingsGUI()
    {
        StoreData[] storeArr = ScriptablesDatabase.Instance.storeList.Values.ToArray();
        foreach (StoreData store in storeArr)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(store.Sprite.texture, GUILayout.Width(60), GUILayout.Height(60));
            GUILayout.Label(store.name);
            if (GUILayout.Button("Edit", GUILayout.Width(60)))
            {
                Selection.activeObject = store;
                EditorGUIUtility.PingObject(store);
            }
            GUILayout.EndHorizontal();
        }
    }
}
