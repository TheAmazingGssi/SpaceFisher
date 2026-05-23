using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public Location StoreType => storeType;
    [SerializeField] private Location storeType;
    public Sprite Sprite => sprite; //TODO: hide if aquarium
    [SerializeField] private Sprite sprite;
    public int Price => price;
    [SerializeField] private int price = 10;
    public float MinInterval => minInterval;
    [SerializeField] private float minInterval = 5;
    public float MaxInterval => maxInterval;
    [SerializeField] private float maxInterval = 15;
}
