using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RatingArg
{
    public string Name;
    [HideInInspector] public float Value;
}

[CreateAssetMenu(fileName = "Rating", menuName = "Scriptable Objects/Rating")]
public class RatingObject : ScriptableObject
{
    [SerializeField] public List<RatingArg> Ratings;
}
