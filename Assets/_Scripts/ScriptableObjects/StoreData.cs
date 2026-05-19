using UnityEngine;

[CreateAssetMenu(fileName = "StoreData", menuName = "Scriptable Objects/StoreData")]
public class StoreData : ScriptableObject
{
    public Store StoreType => storeType;
    [SerializeField] private Store storeType;
    public Sprite Sprite => sprite;
    [SerializeField] private Sprite sprite;

    public int Price => price;
    [SerializeField] private int price;
}
