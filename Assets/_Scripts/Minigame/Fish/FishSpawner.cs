using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject FishObject; //TODO: add depth scriptable object to decide random spawn based on depth
    [SerializeField] MinigameRules minigameRules;
    [SerializeField] HookController hook;
    [SerializeField] FishPool pool;
    [SerializeField] PlanetFishTable planetFishTable;

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
        FishStats[] arr = CheckSpawn(-hook.transform.position.y, hook.DeltaY);
        if (arr.Length > 0)
        {
            foreach (FishStats fish in arr)
            {
                SpawnFish(fish);
            }
        }
    }

    #region Spawn Rate
    public FishStats[] CheckSpawn(float currentHeight, float deltaHeight)
    {
        List<FishStats> spawnList = new List<FishStats>();
        foreach (FishSpawnData fish in planetFishTable.FishSpawnData)
        {
            if (fish.minHeight < currentHeight && currentHeight < fish.maxHeight)
            {
                float spawnChance;
                if (currentHeight > fish.populationPeak)
                    spawnChance = UnLerp(fish.populationPeak, fish.maxHeight, currentHeight);
                else
                    spawnChance = UnLerp(fish.minHeight, fish.populationPeak, currentHeight);

                spawnChance *= deltaHeight * fish.fishType.MaxSpawnRate;
                if (Random.value <= spawnChance)
                    spawnList.Add(fish.fishType);
            }
        }
        return spawnList.ToArray();
    }
    float UnLerp(float min, float max, float value)
    {
        return (value - min) / (max - min);
    }
    #endregion
    #region Spawn Location
    void SpawnFish(FishStats fishStat)
    {
        bool goingRight = Random.Range(0, 2) == 0;
        bool spawnDown = MinigameManager.Instance.Phase == MinigamePhase.Down;
        //FishAI fishAI = FishObject.GetComponent<FishAI>();

        Quaternion rotation;

        //Pick a random position on the hook line
        float randX = Random.Range(-camXBorder, camXBorder);
        //calculate the distance needed to travel to there
        float deltaX = randX + camXBorder; //we assume we start at -camXBorder so  - - camXBorder is +
        float swimTime = deltaX / fishStat.MGSpeed;
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
        if (spawnPoint.y >= -2 * (fishStat.FishSprite.rect.height / fishStat.FishSprite.pixelsPerUnit))
            return;

        pool.Pull(fishStat, spawnPoint, rotation); //TODO: POOLING
        spawnCounter += minigameRules.RandomSpawnDistance;
    }
    void FindCamBorders()
    {
        camXBorder = cam.ViewportToWorldPoint(Vector3.one).x;
    }
    #endregion
}
