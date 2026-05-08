using System;
using UnityEngine;

[Serializable]
public struct RatingArg
{
    public string Name;
    [HideInInspector] public float Value;
}

public abstract class Rating : MonoBehaviour
{
    [SerializeField] protected RatingObject rating;

    public abstract float Grade { get; protected set; }


}
