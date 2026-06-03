using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreProcessBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        //===Build Number===
        string currentVersion = PlayerSettings.bundleVersion;
        string[] parts = currentVersion.Split('.');

        if (parts.Length >= 3 && int.TryParse(parts[2], out int buildNum))
        {
            buildNum++;
            PlayerSettings.bundleVersion = $"{parts[0]}.{parts[1]}.{buildNum}";
        }

        PlayerSettings.Android.bundleVersionCode++;
        Debug.Log($"Build: {PlayerSettings.bundleVersion}");

        //===Remove Debug===
        foreach (var scene in EditorBuildSettings.scenes)
        {
            var scenePath = scene.path;
            Scene currentScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            GameObject debugObj = GameObject.Find("Debug");
        }

        //===Delete Save Data===
        if(File.Exists(Constants.Paths.SaveDataPath))
            File.Delete(Constants.Paths.SaveDataPath);

        if (File.Exists(Constants.Paths.InventoryPath))
            File.Delete(Constants.Paths.InventoryPath);

    }
}
