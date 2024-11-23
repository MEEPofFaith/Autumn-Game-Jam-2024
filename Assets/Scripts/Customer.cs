using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    public CustomerData data;
    public CustomerType type = CustomerType.normal;

    public string customerName(){
        string s = data.customerName;
        if(type != CustomerType.normal){
            s += "?";
        }
        return s;
    }

    public string requestText(){
        if(type == CustomerType.ants){
            return "...";
        }else if(type == CustomerType.parasite){
            // https://stackoverflow.com/questions/18673619/randomizing-a-string

            List<char> original = new List<char>(data.request.ToCharArray());
            List<char> randomized = new List<char>();
            
            for(int size = original.Count; size > 0; size--){
                int index = Random.Range(0, size);
                randomized.Add(original[index]);
                original.RemoveAt(index);
            }

            return new string(randomized.ToArray()) + " (Make it smelly, please!)";
        }

        return data.request;
    }

    public float satisfaction(Meal m){
        data.initPreferences();

        int satisfied = 0;
        foreach(IPreferences preferences in data.preferences){
            if(preferences.valid(m)) satisfied++;
        }
        return (float)satisfied / data.preferences.Length;
    }

    public enum CustomerType{
        normal, ants, parasite
    }
}