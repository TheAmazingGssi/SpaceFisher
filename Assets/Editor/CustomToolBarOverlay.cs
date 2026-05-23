using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[Overlay(typeof(SceneView), "SpaceShipOverlay", true)]
public class CustomToolBarOverlay : Overlay
{
    public override VisualElement CreatePanelContent()
    {
        var root = new VisualElement();

        root.style.flexDirection = FlexDirection.Column;

        var spawnButton = new Button(OnSpawnAquariumClicked) { text = "Spawn Aquarium" };
        root.Add(spawnButton);

        var storeDropdown = new DropdownField("Spawn Store", GetStoreOptions(), 0);
        var spawnStoreButton = new Button(() => OnSpawnStoreClicked(storeDropdown.value)) { text = "Spawn" };
        root.Add(storeDropdown);
        root.Add(spawnStoreButton);

        return root;
    }

    public override void OnCreated()
    {
        base.OnCreated();
        displayed = SceneManager.GetActiveScene().name == Constants.Scenes.Ship;
        EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;
    }

    public override void OnWillBeDestroyed()
    {
        base.OnWillBeDestroyed();
        EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene previous, Scene next)
    {
        displayed = next.name == Constants.Scenes.Ship;
    }

    private List<string> GetStoreOptions()
    {
        return Enum.GetNames(typeof(Location)).Where(name => name != nameof(Location.Aquarium) && name != nameof(Location.Inbetween)).ToList();
    }

    private void OnSpawnStoreClicked(string selectedName)
    {
        if (!Enum.TryParse(selectedName, out Location selectedType)) return;

        StoreData matchingData = AssetDatabase.FindAssets("t:StoreData").Select(guid => AssetDatabase.LoadAssetAtPath<StoreData>(AssetDatabase.GUIDToAssetPath(guid))).FirstOrDefault(data => data.StoreType == selectedType);
        StorePool pool = UnityEngine.Object.FindFirstObjectByType<StorePool>();

        Store store = pool.Get();
        store.Init(matchingData);
        SetPosition(store.gameObject);
        Undo.RegisterCreatedObjectUndo(store.gameObject, "Spawn Store");
        Selection.activeGameObject = store.gameObject;
    }

    private void OnSpawnAquariumClicked()
    {
        AquariumPool pool = UnityEngine.Object.FindFirstObjectByType<AquariumPool>();
        if (pool == null) return;
        Aquarium aquarium = pool.Get();
        SetPosition(aquarium.gameObject);
        Undo.RegisterCreatedObjectUndo(aquarium.gameObject, "Spawn Aquarium");
        Selection.activeGameObject = aquarium.gameObject;
    }

    private void SetPosition(GameObject obj)
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView == null) return;
        Vector2 spawnPos = sceneView.camera.transform.position + sceneView.camera.transform.forward;
        obj.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
    }
}