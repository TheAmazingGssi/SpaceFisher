using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject FishObject; //TODO: add depth scriptable object to decide random spawn based on depth
    [SerializeField] MinigameRules minigameRules;

    private float spawnCounter;

    float camXBorder;
    private void OnValidate()
    {
        cam = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCounter = minigameRules.RandomSpawnTime;
        FindCamBorders();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
            SpawnFish();
    }

    void SpawnFish()
    {
        float xPos;
        Quaternion rotation;
        if(Random.Range(0, 2) == 0)
        {
            xPos = -camXBorder;
            rotation = Quaternion.identity;
        }
        else
        {
            xPos = camXBorder;
            rotation = Quaternion.Euler(0, 180, 0);
        }

        Vector2 spawnPoint = new Vector2(xPos, randomYSpawn());    
        Instantiate(FishObject, spawnPoint, rotation); //TODO: POOLING
        spawnCounter += minigameRules.RandomSpawnTime;
    }
    void FindCamBorders()
    {
        camXBorder = cam.ViewportToWorldPoint(Vector3.one).x;
    }
    float randomYSpawn()
    {
        float maxWorld = 0;
        float minWorld = 0;
        if (MinigameManager.Instance.Phase == MinigamePhase.Down)
        {
            maxWorld = cam.ViewportToWorldPoint(Vector3.one * minigameRules.SpawnRangeMaxDown).y;
            minWorld = cam.ViewportToWorldPoint(Vector3.one * minigameRules.SpawnRangeMinDown).y;
        }
        else
        {
            maxWorld = cam.ViewportToWorldPoint(Vector3.one * minigameRules.SpawnRangeMaxUp).y;
            minWorld = cam.ViewportToWorldPoint(Vector3.one * minigameRules.SpawnRangeMaxUp).y;
        }

        return Random.Range(minWorld, maxWorld);
    }
}
