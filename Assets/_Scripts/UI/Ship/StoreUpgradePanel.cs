using TMPro;
using UnityEngine;

public class StoreUpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private RectTransform trans;
    [SerializeField] private TextMeshProUGUI prevText;
    [SerializeField] private TextMeshProUGUI nextText;
    [SerializeField] private TextMeshProUGUI priceText;

    private StoreBase store;

    private void Start()
    {
        store = null;
        panel.SetActive(false);
        Bus<StorePressed>.OnEvent += OnStorePressed;
    }

    public void OnStorePressed(StorePressed e)
    {
        if(store == e.Store || e.Store == null)
        {
            panel.SetActive(false);
            store = null;
        }
        else
        {
            panel.SetActive(true);
            trans.position = e.Store.uiTrans.position;
            store = e.Store;
            prevText.text = store.CurrentValue.ToString();
            nextText.text = store.Data.Value[store.Level + 1].ToString();
            priceText.text = store.CurrentPrice.ToString();
        }
    }

    public void UpgradeButtonClicked()
    {
        if (CoinsManager.Instance.TryBuying(store.CurrentPrice))
        {
            store.Upgrade();
            panel.SetActive(false);
            store = null;
        }
    }

    private void OnDestroy()
    {
        Bus<StorePressed>.OnEvent -= OnStorePressed;
    }
}
