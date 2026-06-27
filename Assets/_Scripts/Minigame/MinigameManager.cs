using UnityEngine;

public enum MinigamePhase { PreGame, Down, Up }
public class MinigameManager : MonoBehaviour
{
    static public MinigameManager Instance;
    public MinigamePhase Phase;
    public HookController Hook;
    [field:SerializeField] public PlanetFishTable PlanetData {  get; private set; }
    [field:SerializeField] public UpgradeTable UpgradeData {  get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("How did you do that");
            return;
        }

        Instance = this;
    }
    private void OnEnable()
    {
        Bus<MinigameStart>.OnEvent += OnMinigameStart;
        Bus<MinigameEnd>.OnEvent += OnMinigameEnd;
    }
    private void OnDisable()
    {
        Bus<MinigameStart>.OnEvent -= OnMinigameStart;
        Bus<MinigameEnd>.OnEvent -= OnMinigameEnd;
    }
    private void Update()
    {
    }

    void OnMinigameStart(MinigameStart e)
    {
        Phase = MinigamePhase.Down;
    }
    void OnMinigameEnd(MinigameEnd e)
    {
        Phase = MinigamePhase.PreGame;
    }
}
