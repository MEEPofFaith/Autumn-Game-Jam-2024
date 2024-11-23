using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Customer", order = 0)]
public class CustomerData : ScriptableObject {
    public Species species = Species.raccoon;
    public string customerName;
    public string request;

    //Sprites
    public Sprite normal;
    public Sprite ants;
    public Sprite parasite;

    //Preferences
    public string[] preferenceList;
    public IPreferences[] preferences;

    private bool init = false;

    //Parses array of strings into an array of preferences. Cursed, I know.
    public void initPreferences(){
        if(init) return;

        preferences = new IPreferences[preferenceList.Length];
        for(int i = 0; i < preferenceList.Length; i++){
            string[] prefData = preferenceList[i].Split("|");
            if(prefData[0] == "ing"){ //First is type
                IngredientPreference pref = new IngredientPreference();
                pref.requested = prefData[1]; //Second is name of ingredient
                pref.minRequired = int.Parse(prefData[2]); //Third is amount
                preferences[i] = pref;
            }else if(prefData[0] == "stat"){
                StatPreference pref = new StatPreference();
                pref.stat = (IngredientData.IngredientStat)int.Parse(prefData[1]); //Second is stat
                pref.min = float.Parse(prefData[2]); //Third is min
                pref.max = float.Parse(prefData[3]); //Fourth is max
                preferences[i] = pref;
            }
        }
        init = true;
    }

    public enum Species{
        raccoon, rat, opossum, squirrel, skunk, bird
    }
}