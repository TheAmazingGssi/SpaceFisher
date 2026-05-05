using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class FishButton : MonoBehaviour
{
    [SerializeField] private Image fishImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button button;
    [SerializeField] private FishManager fish;

    private FishStats fishStats;

    public void Setup(FishStats stats, int count)
    {
        Debug.Log("Count in refresh button: " + count);
        fishStats = stats;
        fishImage.sprite = stats.FishSprite;
        countText.text = count.ToString();
        gameObject.SetActive(true);
    }
    public void OnButtonClick()
    {
        fish.Initialize(fishStats);
        Inventory.Instance.TryRemoveFish(fishStats);
        Bus<PlaceFish>.Raise(new PlaceFish { Fish = fish });
    }

    public void Hide()
    {
        gameObject.SetActive(false);    
    }
}

