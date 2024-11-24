using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Customer", order = 0)]
public class CustomerData : ScriptableObject {
    public string customerName;
    public string request;
    public bool normalOnly = false;
    public CustomerType type = CustomerType.normal;

    //Sprites
    public Sprite normal;
    public Sprite ants;
    public Sprite parasite;

    //Preferences
    public string[] preferenceList;
    public IPreferences[] preferences;

    public string fail;
    public string pass;
    public string[] passCond;
    public string success;

    private bool init = false;

    public string nameText(){
        if(type != CustomerType.normal){
            return name + "?";
        }
        return name;
    }

    public string requestText(){
        if(type == CustomerType.ants){
            return "...";
        }
        string text = request;
        if(type == CustomerType.parasite) text = scrambleText(text);
        return text;
    }

    public string failText(){
        if(type == CustomerType.ants){
            return "...";
        }
        string text = fail;
        if(type == CustomerType.parasite) text = scrambleText(text);
        return text;
    }

    public string passText(int failed){
        if(type == CustomerType.ants){
            return "...";
        }
        string text = pass + " (" + passCond[failed] + ")";
        if(type == CustomerType.parasite) text = scrambleText(text);
        return text;
    }

    public string successText(){
        if(type == CustomerType.ants){
            return "...";
        }
        string text = success;
        if(type == CustomerType.parasite) text = scrambleText(text);
        return text;
    }

    public void setType(CustomerType type){
        if(normalOnly){
            this.type = CustomerType.normal;
        }else{
            this.type = type;
        }
    }

    //Parses array of strings into an array of preferences. Cursed, I know.
    public void initPreferences(){
        if(init) return;

        preferences = new IPreferences[preferenceList.Length];
        for(int i = 0; i < preferenceList.Length; i++){
            string[] prefData = preferenceList[i].Split("|");
            if(prefData[0] == "ing"){ //First is type
                IngredientPreference pref = new IngredientPreference();
                pref.requested = prefData[1]; //Second is name of ingredient
                if(prefData.Length == 3) pref.minRequired = int.Parse(prefData[2]); //Third is amount
                preferences[i] = pref;
            }else if(prefData[0] == "stat"){
                StatPreference pref = new StatPreference();
                pref.stat = (IngredientData.IngredientStat)int.Parse(prefData[1]); //Second is stat
                pref.min = float.Parse(prefData[2]); //Third is min
                pref.max = float.Parse(prefData[3]); //Fourth is max
                preferences[i] = pref;
            }else if(prefData[0] == "nol"){
                preferences[i] = new NoLunchlyPreference();
            }
        }
        init = true;
    }

    public static string scrambleText(string toScramble){
        // https://stackoverflow.com/questions/18673619/randomizing-a-string

        List<char> original = new List<char>(toScramble.ToCharArray());
        List<char> randomized = new List<char>();
        
        for(int size = original.Count; size > 0; size--){
            int index = UnityEngine.Random.Range(0, size);
            randomized.Add(original[index]);
            original.RemoveAt(index);
        }

        return new string(randomized.ToArray());
    }

    public enum CustomerType{
        normal, ants, parasite
    }
}