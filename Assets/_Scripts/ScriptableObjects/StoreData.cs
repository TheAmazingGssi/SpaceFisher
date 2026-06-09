using UnityEngine;

[CreateAssetMenu(fileName = "StoreData", menuName = "Scriptable Objects/BuildingData/StoreData")]
public class StoreData : BuildingData
{
    public Sprite[] Sprites => sprites;
    [SerializeField] protected Sprite[] sprites;
    public int Level => level;
    [SerializeField] protected int level = 0;
    public int[] Price => price;
    [SerializeField] private int[] price;
    public int[] Value => value;
    [SerializeField] private int[] value;


}
