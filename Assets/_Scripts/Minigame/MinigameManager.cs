using UnityEngine;

public enum MinigamePhase { PreGame, Down, Up }
public class MinigameManager : MonoBehaviour
{
    static public MinigameManager Instance;
    public MinigamePhase Phase;
    public HookController Hook;
    public PlanetFishTable PlanetData;

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
        if (Hook.transform.position.y <= -PlanetData.MaxDepth)
            Phase = MinigamePhase.Up;
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
