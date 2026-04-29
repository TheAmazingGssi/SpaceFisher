using UnityEngine;

[CreateAssetMenu(fileName = "FishStats", menuName = "Scriptable Objects/FishStats")]
public class FishStats : ScriptableObject
{
    [field:SerializeField] public float Speed { get; private set; }
    [field:SerializeField] public float Height { get; private set; }
}
