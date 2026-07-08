using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeType", menuName = "Scriptable Objects/Upgrades/UpgradeType")]
public class UpgradeType : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public float[] MinigameValue { get; private set; }
    [field: SerializeField] public float[] MoneyCost { get; private set; }

    public int MaxLevel => MinigameValue.Length - 1;
}