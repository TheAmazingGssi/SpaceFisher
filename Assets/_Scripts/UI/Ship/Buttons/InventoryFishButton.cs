using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFishButton : FishButton
{
    public override void OnButtonClick()
    {
        base.OnButtonClick();
        Bus<PlaceFish>.Raise(new PlaceFish { Fish = fishStats });
    }
}