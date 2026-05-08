using UnityEngine;

[CreateAssetMenu(fileName = "VisitorData", menuName = "Scriptable Objects/VisitorData")]
public class VisitorData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite;
}
