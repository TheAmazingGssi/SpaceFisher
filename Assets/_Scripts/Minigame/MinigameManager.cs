using UnityEngine;

public enum MinigamePhase { PreGame, Down, Up }
public class MinigameManager : MonoBehaviour
{
    static public MinigameManager Instance;
    public MinigamePhase Phase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
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

    void OnMinigameStart(MinigameStart e)
    {
        Phase = MinigamePhase.Down;
    }
    void OnMinigameEnd(MinigameEnd e)
    {
        Phase = MinigamePhase.PreGame;
    }
}
