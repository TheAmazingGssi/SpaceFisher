using UnityEngine;

public class Restaurant : StoreBase
{
    public override Store Zone => Store.Restaurant;

    protected override void NewVisitor(Visitor visitor)
    {
        Debug.Log("Visitor in restaurant");
    }

}
