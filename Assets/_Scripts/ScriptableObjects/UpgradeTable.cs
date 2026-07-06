using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTable", menuName = "Scriptable Objects/Upgrades/UpgradeTable")]
public class UpgradeTable : ScriptableObject
{
    [field:SerializeField] public int[] MaxDepth { get; private set; }
    [field: SerializeField] public float[] MagnetRadius { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}
