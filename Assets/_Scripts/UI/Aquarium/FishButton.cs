using UnityEngine;

public class FishButton : MonoBehaviour
{
    [SerializeField] private FishManager fish;
    public void OnButtonClick()
    {
        Bus<PlaceFish>.Raise(new PlaceFish { Fish = fish });
    }
}
