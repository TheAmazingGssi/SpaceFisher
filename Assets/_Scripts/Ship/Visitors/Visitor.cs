using UnityEngine;

public enum StoreType
{
    Aquarium,
    Restaurant,
    Theater,
    GiftShop,
    Inbetween
}
public class Visitor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    public StoreType CurrentStore {  get; private set; }

    public void Initialize(VisitorData data)
    {
        renderer.sprite = data.Sprite;
    }

    [ContextMenu("Add to restaurant")]
    private void ChangeZone()
    {
        CurrentStore = StoreType.Restaurant;
        Bus<ChangeLocation>.Raise(new ChangeLocation {  Visitor = this });
    }
}
