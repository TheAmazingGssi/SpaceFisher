using System;
using UnityEngine;

public abstract class Rating : MonoBehaviour
{
    [SerializeField] protected RatingObject rating;

    public abstract float Grade { get; protected set; }


}
