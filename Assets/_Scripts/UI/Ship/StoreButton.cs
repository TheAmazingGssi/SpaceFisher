using UnityEngine;

public class StoreButton : ItemButton<StoreBase>
{
    private StoreBase store;
    public override void Setup(StoreBase data)
    {
        store = data;
        Debug.Log(store.gameObject.name);
    }
}
