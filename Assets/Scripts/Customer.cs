using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    public static Customer Instance;

    public CustomerState state = CustomerState.entry;
    public float speed;

    private CustomerData data;

    private void Awake() {
        if(Instance != null) Debug.LogError("Why are there two customer instances?");
        Instance = this;
    }

    private void Update() {
        if(state == CustomerState.entry){
            float pos = gameObject.transform.position.x - speed * Time.deltaTime;
            if(pos < 0){
                pos = 0;
                state = CustomerState.wait;
            }
            gameObject.transform.position = new Vector3(pos, gameObject.transform.position.y, gameObject.transform.position.z);
        }else if(state == CustomerState.exit){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    public bool arrived(){
        return gameObject.transform.position.x == 0;
    }

    public bool exited(){
        return gameObject.transform.position.x > 11;
    }

    public void init(CustomerData data){
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        switch(data.type){
            case CustomerData.CustomerType.normal:
                renderer.sprite = data.normal;
                break;
            case CustomerData.CustomerType.ants:
                renderer.sprite = data.ants;
                break;
            case CustomerData.CustomerType.parasite:
                renderer.sprite = data.parasite;
                break;
        }

        gameObject.transform.position = new Vector3(11, gameObject.transform.position.y, gameObject.transform.position.z);
        state = CustomerState.entry;
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
            for(int i = 0; i < len; i++){
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
            var nol = pref as NoLunchlyPreference;
            if(nol != null && !pref.valid(m)) return 0; //INSTANT FAIL
            if(pref.valid(m)) passed++;
        }
        if(passed == data.preferences.Length) passed++; //Bonus for completing all.
        return passed * 2;
    }

    public enum CustomerState{
        entry, wait, exit
    }
}