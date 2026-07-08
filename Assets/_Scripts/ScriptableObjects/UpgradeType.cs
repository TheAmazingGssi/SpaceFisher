using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeType", menuName = "Scriptable Objects/Upgrades/UpgradeTable")]
public class UpgradeType : ScriptableObject
{
    [field:SerializeField] public float[] MinigameValue {  get; private set; }
    [field:SerializeField] public float[] MoneyCost {  get; private set; }
}
