using System.Collections.Generic;

public class AquariumFishPool : ObjectPool<FishManager>
{
    private List<FishManager> fishPool = new List<FishManager>();

    public FishManager Get(FishManager fish)
    {
        for (int i = 0; i < fishPool.Count; i++)
        {
            FishManager f = fishPool[i];

            if (!f.gameObject.activeSelf && f == fish)
            {
                f.gameObject.SetActive(true);
                return f;
            }
        }

        FishManager newFish = Instantiate(fish, transform);
        newFish.gameObject.SetActive(true);

        fishPool.Add(newFish);
        return newFish;
    }

    public override void Release(FishManager obj)
    {
        obj.gameObject.SetActive(false);
    }
}
