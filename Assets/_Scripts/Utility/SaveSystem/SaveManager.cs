using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    const string SAVE_PATH = "/gameState.json";

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
        Load();
    }

    private void OnDestroy()
    {
        Bus<SaveData>.OnEvent -= OnSaveRequested;
    }

    private void OnApplicationQuit()
    {
        Save();
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
        System.IO.File.WriteAllText(Application.persistentDataPath + SAVE_PATH, json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + SAVE_PATH;
        if (!System.IO.File.Exists(path)) return;
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(System.IO.File.ReadAllText(path));
        RunManager.Instance.RestoreFrom(data);
        Bus<LoadData>.Raise(new LoadData { Data = data });
    }
}