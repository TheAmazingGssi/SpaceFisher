using UnityEngine;

public enum MinigamePhase { PreGame, Down, Up }
public class MinigameManager : MonoBehaviour
{
    static public MinigameManager Instance;
    public MinigamePhase Phase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
