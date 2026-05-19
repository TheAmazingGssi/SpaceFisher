using UnityEngine;

[CreateAssetMenu(fileName = "StoreData", menuName = "Scriptable Objects/StoreData")]
public class StoreData : ScriptableObject
{
    public StoreType StoreType => storeType;
    [SerializeField] private StoreType storeType;
    public Sprite Sprite => sprite;
    [SerializeField] private Sprite sprite;
    public int Price => price;
    [SerializeField] private int price = 10;
    public float MinInterval => minInterval;
    [SerializeField] private float minInterval = 5;
    public float MaxInterval => maxInterval;
    [SerializeField] private float maxInterval = 15;
}
