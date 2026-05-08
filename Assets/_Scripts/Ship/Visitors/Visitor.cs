using UnityEngine;

public enum Zone
{
    Line,
    Aquarium,
    Food,
    TicketBooth,
    Inbetween
}
public class Visitor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    public Zone CurrentZone {  get; private set; }

    public void Initialize(VisitorData data)
    {
        renderer.sprite = data.Sprite;
    }

    private void ChangeZone(Zone area)
    {
        CurrentZone = area;
        Bus<ChangeLocation>.Raise(new ChangeLocation {  Visitor = this });
    }
}
