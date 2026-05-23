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
    string searchQuary;
    bool useFishFilter;
    [SerializeField]
    private Planet fishFilter;

    private SerializedObject so;
    private SerializedProperty fishFilterProp;

    private void OnEnable()
    {
        so = new SerializedObject(this);
        fishFilterProp = so.FindProperty("fishFilter");
    }

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
        searchQuary = EditorGUILayout.TextField("Filter: ", searchQuary);

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
        useFishFilter = EditorGUILayout.Toggle("Use Planet as Filter", useFishFilter);
        if (useFishFilter)
        {
            so.Update();
            EditorGUILayout.PropertyField(fishFilterProp);
            so.ApplyModifiedProperties();
        }


        EditorGUILayout.Space();
        FishStats[] fishArr = ScriptablesDatabase.Instance.fishList.Values.ToArray();
        foreach(FishStats fish in fishArr)
            if(fish.name.ToLower().Contains(searchQuary.ToLower()) && (!useFishFilter || fish.Planet == fishFilter))
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
        EditorGUILayout.Space();
        BuildingData[] storeArr = ScriptablesDatabase.Instance.storeList.Values.ToArray();
        foreach (BuildingData store in storeArr)
            if (store.name.ToLower().Contains(searchQuary.ToLower()))
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
