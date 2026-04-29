using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject FishObject; //TODO: add depth scriptable object to decide random spawn based on depth
    [SerializeField] MinigameRules minigameRules;
    [SerializeField] Transform hook;

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
            RandomSpawn();
    }

    void SpawnFish()
    {
        RandomSpawn();
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

    void RandomSpawn()
    {
        bool goingRight = Random.Range(0, 2) == 0;
        bool spawnFromSide = Random.Range(0, 2) == 0;
        bool spawnDown = MinigameManager.Instance.Phase == MinigamePhase.Down;
        FishAI fishAI = FishObject.GetComponent<FishAI>();

        Vector2 spawnPoint = Vector2.zero;
        Quaternion rotation;

        if (spawnFromSide)
        {
            float yMin = cam.ViewportToWorldPoint(Vector3.zero).y;
            float yMax = cam.ViewportToWorldPoint(Vector3.one * 0.5f).y;
            spawnPoint.y = Random.Range(yMin, yMax);
            spawnPoint.x = -camXBorder;
        }
        else
        {
            spawnPoint.y = cam.ViewportToWorldPoint(Vector3.zero).y - (fishAI.Stats.Height * 0.5f);
            float xMin = cam.ViewportToWorldPoint(Vector3.zero).x;
            float xMax = cam.ViewportToWorldPoint(Vector3.one).x;
            spawnPoint.x = Random.Range(xMin, xMax);
        }

        if(!goingRight)
        {
            spawnPoint.x = (2*hook.position.x) - spawnPoint.x;
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else
            rotation = Quaternion.identity;

        if (!spawnDown)
        {
            spawnPoint.y = (2 * hook.position.y) - spawnPoint.y;
        }

        Instantiate(FishObject, spawnPoint, rotation); //TODO: POOLING
        spawnCounter += minigameRules.RandomSpawnTime;
    }
}
