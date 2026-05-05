using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject FishObject; //TODO: add depth scriptable object to decide random spawn based on depth
    [SerializeField] MinigameRules minigameRules;
    [SerializeField] HookController hook;

    private float spawnCounter;

    float camXBorder;
    private void OnValidate()
    {
        cam = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCounter = minigameRules.RandomSpawnDistance;
        FindCamBorders();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= hook.DeltaY;
        if (spawnCounter <= 0)
            SpawnFish();
    }

    void SpawnFish()
    {
        bool goingRight = Random.Range(0, 2) == 0;
        bool spawnFromSide = Random.Range(0, 2) == 0;
        bool spawnDown = MinigameManager.Instance.Phase == MinigamePhase.Down;
        FishAI fishAI = FishObject.GetComponent<FishAI>();

        Vector2 spawnPoint = Vector2.zero;
        Quaternion rotation;

        if (spawnFromSide)
        {
            //Get random Y on edge of screen's lower half
            float yMin = cam.ViewportToWorldPoint(Vector3.zero).y;
            float yMax = cam.ViewportToWorldPoint(Vector3.one * 0.5f).y;
            spawnPoint.y = Random.Range(yMin, yMax);
            spawnPoint.x = -camXBorder;
        }
        else
        {
            //Get random x for edge of screen's bottom
            spawnPoint.y = cam.ViewportToWorldPoint(Vector3.zero).y - (fishAI.Stats.FishSprite.rect.height / fishAI.Stats.FishSprite.pixelsPerUnit * 0.5f);
            float xMin = cam.ViewportToWorldPoint(Vector3.zero).x;
            float xMax = cam.ViewportToWorldPoint(Vector3.one).x;
            spawnPoint.x = Random.Range(xMin, xMax);
        }

        if (!goingRight)
        {
            //rotate the fish and mirror the spawn vertically
            spawnPoint.x = (2 * hook.transform.position.x) - spawnPoint.x;
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else
            rotation = Quaternion.identity;

        if (!spawnDown)
        {
            //mirror the spawn horizontally
            spawnPoint.y = (2 * hook.transform.position.y) - spawnPoint.y;
        }

        if (spawnPoint.y >= -2* (fishAI.Stats.FishSprite.rect.height / fishAI.Stats.FishSprite.pixelsPerUnit))
            return;

        Instantiate(FishObject, spawnPoint, rotation); //TODO: POOLING
        spawnCounter += minigameRules.RandomSpawnDistance;
    }
    void FindCamBorders()
    {
        camXBorder = cam.ViewportToWorldPoint(Vector3.one).x;
    }
}
