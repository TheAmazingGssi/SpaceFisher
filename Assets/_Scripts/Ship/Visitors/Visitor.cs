using UnityEngine;

public enum Area
{
    Line,
    Aquarium,
    Food,
    Facilities,
    Inbetween
}
public class Visitor : MonoBehaviour
{
    public Area CurrentArea {  get; private set; }
    public void ChangeArea(Area area)
    {
        CurrentArea = area;
    }


}
