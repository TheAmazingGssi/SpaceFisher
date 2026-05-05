using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishInventoryButton : MonoBehaviour
{
    [SerializeField] private Image fishImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button button;

    private FishStats fishStats;

    public void Setup(FishStats stats, int count)
    {
        fishStats = stats;
        fishImage.sprite = stats.FishSprite;
        countText.text = count.ToString();
        gameObject.SetActive(true);
    }

    public void UpdateCount(int count)
    {
        countText.text = count.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}