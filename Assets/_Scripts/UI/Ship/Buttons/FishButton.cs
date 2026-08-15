using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishButton : ItemButton<KeyValuePair<FishStats, int>>
{
    [SerializeField] protected Image[] stars;

    [Header("Slider")]
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI selectedAmountText;

    protected int amount;
    protected int fishNum;
    protected FishStats fishStats;

    private void Start()
    {
        slider.onValueChanged.AddListener(ChangeValue);
    }

    private void ChangeValue(float value)
    {
        selectedAmountText.text = value.ToString();
    }

    public override void Setup(KeyValuePair<FishStats, int> data)
    {
        gameObject.SetActive(true);
        fishStats = data.Key;
        amount = data.Value;
        text.text = amount.ToString();
        slider.maxValue = amount;
        image.sprite = data.Key.FishSprite;
        slider.value = slider.minValue;
        selectedAmountText.text = slider.value.ToString();

        for (int i = 0; i < stars.Length; i++)
            stars[i].gameObject.SetActive(false);
        for (int i = 0; i < fishStats.Value; i++)
            stars[i].gameObject.SetActive(true);
    }

    public override void OnButtonClick()
    {
        fishNum = (int)slider.value;
        if (!Inventory.Instance.TryRemoveFish(fishStats, fishNum)) return;

        amount -= fishNum;
        slider.maxValue = amount;
        text.text = amount.ToString();
    }
}