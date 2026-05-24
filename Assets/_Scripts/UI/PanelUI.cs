using System.Collections.Generic;
using UnityEngine;

public abstract class PanelUI<TButton, TData> : MonoBehaviour where TButton : ItemButton<TData>
{
    [SerializeField] protected Transform itemSection;
    [SerializeField] protected TButton buttonPrefab;

    private readonly List<TButton> buttonPool = new();

    public virtual void RefreshPanel(IEnumerable<TData> items)
    {
        int index = 0;
        foreach (TData item in items)
        {
            GetButton(index).Setup(item);
            index++;
        }
        for (int i = index; i < buttonPool.Count; i++)
            buttonPool[i].Hide();
    }

    private TButton GetButton(int index)
    {
        if (index < buttonPool.Count)
            return buttonPool[index];

        TButton newButton = Instantiate(buttonPrefab, itemSection);
        buttonPool.Add(newButton);
        return newButton;
    }
}
public abstract class ItemButton<TData> : CustomButton
{
    public abstract void Setup(TData data);
}