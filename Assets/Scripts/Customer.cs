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
            return "uugghhhhhh...";
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