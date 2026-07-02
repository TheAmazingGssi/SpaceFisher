using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTable", menuName = "Scriptable Objects/UpgradeTable")]
public class UpgradeTable : ScriptableObject
{
    [field:SerializeField] public int[] MaxDepth { get; private set; }
    [field: SerializeField] public float[] MagnetRadius { get; private set; }
}
