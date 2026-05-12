using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MenuItems : MonoBehaviour
{
    private const string minigame = "Assets/_Prefabs/MinigameFish.prefab";
    private const string aquarium = "Assets/_Prefabs/AquariumFish.prefab";

    [MenuItem("Tools/Go To Minigame Fish %e")]
    private static void OpenMinigameFishPrefab()
    {
        GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(minigame);
        AssetDatabase.OpenAsset(prefabAsset);
    }

    [MenuItem("Tools/Go To Aquarium Fish %w")]
    private static void OpenAquariumFishPrefab()
    {
        GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(aquarium);
        AssetDatabase.OpenAsset(prefabAsset);
    }

    [MenuItem("Tools/Clear Inventory %&q")]
    private static void ClearInventory()
    {
        Object.FindFirstObjectByType<Inventory>().ClearInventory();
    }

    [MenuItem("Tools/Switch Scene %q")]
    private static void SwitchScene()
    {
        string currentScene = EditorSceneManager.GetActiveScene().name ==
            Constants.Scenes.Aquarium ? Constants.Scenes.Minigame : Constants.Scenes.Aquarium;
        EditorSceneManager.OpenScene("Assets/Scenes/" + currentScene + ".unity");
    }
}
