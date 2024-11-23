using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Customer", order = 0)]
public class CustomerData : ScriptableObject {
    public Species species = Species.raccoon;
    public AnimalState type = AnimalState.normal;
    public string animalName;
    public string request;


    public enum Species{
        raccoon, rat, opossum, squirrel, skunk, bird
    }

    public enum AnimalState{
        normal, ants, parasite
    }
}