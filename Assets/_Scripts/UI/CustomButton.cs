using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected Button button;

    public virtual void OnButtonClick() { }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
