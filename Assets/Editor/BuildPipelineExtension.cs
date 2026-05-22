#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
[InitializeOnLoad]
public class BuildPipelineExtension
{
    static BuildPipelineExtension()
    {
        BuildPlayerWindow.RegisterBuildPlayerHandler(OnBuild);
    }
    static void OnBuild(BuildPlayerOptions options)
    {
        var constantScenes = typeof(Constants).GetNestedType("Scenes").GetFields().Select(f => f.GetValue(null) as string).ToHashSet();
        var buildScenes = options.scenes.Select(System.IO.Path.GetFileNameWithoutExtension).ToList();
        bool mismatch = buildScenes.Any(s => !constantScenes.Contains(s)) || constantScenes.Any(s => !buildScenes.Contains(s));
        if (mismatch)
            if (EditorUtility.DisplayDialog("Scene mismatch!", "Choose fix to only have scenes that exist in the Constant class", "Fix & Continue", "Continue without fixing"))
                options.scenes = constantScenes.Select(name => AssetDatabase.FindAssets($"{name} t:Scene").Select(AssetDatabase.GUIDToAssetPath).FirstOrDefault(p => System.IO.Path.GetFileNameWithoutExtension(p) == name)).Where(p => p != null).ToArray();

        var fishWithoutSprite = FishTypeList.Instance.list.Values.Where(f => f.FishSprite == null).ToList();
        if (fishWithoutSprite.Count > 0)
            if (!EditorUtility.DisplayDialog("Missing fish sprites!", "Some of the fish have no sprite. Do you wish to continue?", "Continue Anyway", "Cancel Build"))
                return;

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
    }
}
#endif