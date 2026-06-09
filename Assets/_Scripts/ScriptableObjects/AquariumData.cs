using UnityEngine;

[CreateAssetMenu(fileName = "AquariumData", menuName = "Scriptable Objects/BuildingData/AquariumData")]
public class AquariumData : BuildingData
{
    public int Price => price;
    [SerializeField] private int price = 10;
}
