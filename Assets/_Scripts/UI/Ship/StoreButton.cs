using UnityEngine;

public class StoreButton : ItemButton<StoreData>
{
    private StoreData store;
    public override void Setup(StoreData data)
    {
        store = data;
        image.sprite = store.Sprite;
    }

    public override void OnButtonClick()
    {
        if (CoinsManager.Instance.TryBuying(store.Price))
            Bus<StoreBought>.Raise(new StoreBought { Data = store });
        else
            Debug.Log("Not enough coins");

    }
}
