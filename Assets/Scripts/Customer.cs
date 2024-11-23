using UnityEngine;

public class Customer : MonoBehaviour {
    public CustomerData data;

    public float satisfaction(Meal m){
        data.initPreferences();

        int satisfied = 0;
        foreach(IPreferences preferences in data.preferences){
            if(preferences.valid(m)) satisfied++;
        }
        return (float)satisfied / data.preferences.Length;
    }
}