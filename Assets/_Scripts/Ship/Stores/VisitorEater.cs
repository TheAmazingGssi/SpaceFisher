using UnityEngine;

public class VisitorEater : StoreBase
{
    public override Vector2 GetEntryPoint(Collider2D col, Vector2 visitorPos)
    {
        return new Vector2(col.bounds.center.x, visitorPos.y);
    }

}
