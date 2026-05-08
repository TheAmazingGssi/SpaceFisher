using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rating", menuName = "Scriptable Objects/Rating")]
public class RatingObject : ScriptableObject
{
    [SerializeField] public List<RatingArg> Ratings;
}
