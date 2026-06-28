using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Bus<SaveData>.OnEvent += OnSaveRequested;
#if !UNITY_EDITOR
        Load();
#endif
    }

    private void OnDestroy()
    {
        Bus<SaveData>.OnEvent -= OnSaveRequested;
    }

    private void OnApplicationQuit()
    {
        Save();
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey(Constants.FirstOpen);
#endif
    }
    private void OnApplicationPause(bool paused)
    {
        if (paused) Save(); 
    }
    private void OnSaveRequested(SaveData e) => Save();

    public void Save()
    {
        if (!RunManager.Instance) return;
        GameSaveData data = RunManager.Instance.Snapshot();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + Constants.Paths.SaveDataPath, json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + Constants.Paths.SaveDataPath;
        if (!File.Exists(path)) return;
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(path));
        RunManager.Instance.RestoreFrom(data);
        Bus<LoadData>.Raise(new LoadData { Data = data });
    }

    [ContextMenu("DELETE")]
    public void Delete() //TODO: check why not deleting
    {
        File.Exists(Constants.Paths.SaveDataPath);
        File.Delete(Constants.Paths.SaveDataPath);
        File.Exists(Constants.Paths.SaveDataPath);
        File.Delete(Constants.Paths.SaveDataPath);
    }
}