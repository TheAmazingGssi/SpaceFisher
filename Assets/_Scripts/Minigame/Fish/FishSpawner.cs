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
        bool spawnDown = MinigameManager.Instance.Phase == MinigamePhase.Down;
        FishAI fishAI = FishObject.GetComponent<FishAI>();

        Quaternion rotation;
        
        //Pick a random position on the hook line
        float randX = Random.Range(-camXBorder, camXBorder);
        //calculate the distance needed to travel to there
        float deltaX = randX + camXBorder; //we assume we start at -camXBorder so  - - camXBorder is +
        float swimTime = deltaX / fishAI.Stats.MGSpeed;
        float height = hook.transform.position.y - (hook.Speed * swimTime);

        Vector2 spawnPoint = new Vector2(-camXBorder, height);
        

        //flip left right
        if (!goingRight)
        {
            //rotate the fish and mirror the spawn vertically
            spawnPoint.x = (2 * hook.transform.position.x) - spawnPoint.x;
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else
            rotation = Quaternion.identity;

        //spawn above or under the hook
        if (!spawnDown)
        {
            //mirror the spawn horizontally
            spawnPoint.y = (2 * hook.transform.position.y) - spawnPoint.y;
        }

        //Dont spawn if too close to ship (assumes the ship is at y=0)
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
