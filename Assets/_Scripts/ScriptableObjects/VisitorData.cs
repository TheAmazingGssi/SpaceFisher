using UnityEngine;

[CreateAssetMenu(fileName = "VisitorData", menuName = "Scriptable Objects/VisitorData")]
public class VisitorData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite;
    [field: SerializeField] public float MoveSpeed = 2;
    [field: SerializeField][field: Range(0, 1)] public float EnterBuildingChance = 0.5f;
}
