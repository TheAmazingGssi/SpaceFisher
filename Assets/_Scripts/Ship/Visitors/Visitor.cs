using UnityEngine;

public enum Location
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
    public Location CurrentLocation {  get; private set; }

    public void Initialize(VisitorData data)
    {
        renderer.sprite = data.Sprite;
    }

    [ContextMenu("Add to restaurant")]
    private void ChangeLocation()
    {
        CurrentLocation = Location.Restaurant;
        Bus<ChangeLocation>.Raise(new ChangeLocation {  Visitor = this });
    }
}
