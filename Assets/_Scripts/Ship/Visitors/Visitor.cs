using UnityEngine;

public enum Store
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
    public Store CurrentStore {  get; private set; }

    public void Initialize(VisitorData data)
    {
        renderer.sprite = data.Sprite;
    }

    [ContextMenu("Add to restaurant")]
    private void ChangeZone()
    {
        CurrentStore = Store.Restaurant;
        Bus<ChangeLocation>.Raise(new ChangeLocation {  Visitor = this });
    }
}
