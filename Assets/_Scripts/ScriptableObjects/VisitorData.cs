using UnityEngine;

[CreateAssetMenu(fileName = "VisitorData", menuName = "Scriptable Objects/VisitorData")]
public class VisitorData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite {  get; private set; }
    [field: SerializeField][field: Range(0, 1)] public float EnterBuildingChance { get; private set; } = 0.5f;
}
