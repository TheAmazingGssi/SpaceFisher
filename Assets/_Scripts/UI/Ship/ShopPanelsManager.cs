using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanelsManager : MonoBehaviour
{
    [SerializeField] private ShopButton[] shopButtons;

    [SerializeField] private StoreShopUI shopPanel;
    [SerializeField] private UpgradeShopUI upgradePanel;

     private GameObject[] panels = new GameObject[2];

    [SerializeField] private ShopButton[] buttons;

    private void Awake()
    {
        Bus<ShopButtonPressed>.OnEvent += OnShopButtonClicked;

         panels[0] = shopPanel.gameObject;
         panels[1] = upgradePanel.gameObject;
    }

    private void Start()
    {
        OnShopButtonClicked(new ShopButtonPressed { ShopButton = buttons[0] });
    }

    private void OnShopButtonClicked(ShopButtonPressed e)
    {
        foreach (var b in shopButtons)
        {
            if (b != e.ShopButton)
                b.Button.interactable = true;
            e.ShopButton.Button.interactable = false;
        }

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].Panel.SetActive(false);

        e.ShopButton.Panel.SetActive(true);
    }

    private void OnDestroy()
    {
        Bus<ShopButtonPressed>.OnEvent -= OnShopButtonClicked;
    }
}
