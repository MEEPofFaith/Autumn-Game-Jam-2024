using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Customer", order = 0)]
public class CustomerData : ScriptableObject {
    public Species species;
    public Type type;
    public string animalName;
    public string request;
    public bool isAnts;


    public enum Species{
        raccoon, rat, opossum, squirrel, skunk, bird
    }

    public enum Type{
        normal, ants, parasite
    }
}