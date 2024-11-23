using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    public CustomerData data;

    public void init(CustomerData data){
        data.initPreferences();
        //TODO randomize ant/parasite
        //TODO set sprite
    }

    public string feedback(Meal m){
        int len = data.preferences.Length;
        bool[] complete = new bool[len];
        bool pass = false;
        for(int i = 0; i < len; i++){
            bool passPref = data.preferences[i].valid(m);
            if(passPref) pass = true;
            complete[i] = pass;
        }

        if(pass){
            for(int i = 0; i < len, i++){
                if(!complete[i]){
                    return data.passText(i);
                }
            }
            return data.successText();
        }else{
            return data.failText();
        }
    }

    public int score(Meal m){
        int passed = 0;
        foreach(IPreferences pref in data.preferences){
            if(pref.valid(m)) passed++;
        }
        if(passed == data.preferences.Length) passed++; //Bonus for completing all.
        return passed * 2;
    }
}