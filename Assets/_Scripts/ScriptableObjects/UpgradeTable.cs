using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTable", menuName = "Scriptable Objects/UpgradeTable")]
public class UpgradeTable : ScriptableObject
{
    public int[] MaxDepth { get; private set; } 
    public float[] MagnetRadius { get; private set; }
}
