using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour {
    public static Customer Instance;

    public CustomerState state = CustomerState.entry;
    public float speed;
    public GameObject dialogBox;
    public TMP_Text dialog;

    private CustomerData data;
    private float delayTimer = 2f;

    private void Awake() {
        if(Instance != null) Debug.LogError("Why are there two customer instances?");
        Instance = this;
    }

    private void Update() {
        float pos;
        switch(state){
            case CustomerState.entry:
                pos = gameObject.transform.position.x - speed * Time.deltaTime;
                if(arrived()){
                    pos = 0;
                    state = CustomerState.wait;

                    dialogBox.SetActive(true);
                    dialog.SetText(data.requestText());
                }
                gameObject.transform.position = new Vector3(pos, gameObject.transform.position.y, gameObject.transform.position.z);
                break;
            case CustomerState.wait:
                if(Plate.Instance.arrived()){
                    state = CustomerState.served;

                    dialog.SetText(feedback(MainManager.Instance.currentMeal));
                    MainManager.Instance.serveMeal();

                    delayTimer = 2f;
                }
                break;
            case CustomerState.served:
                if(delayTimer > 0 || !CameraManager.Instance.returned()){
                    delayTimer -= Time.deltaTime;
                    break;
                }

                state = CustomerState.exit;
                Plate.Instance.toggle(true);
                Pot.Instance.toggle(false);

                break;
            case CustomerState.exit:
                pos = gameObject.transform.position.x + speed * Time.deltaTime;
                gameObject.transform.position = new Vector3(pos, gameObject.transform.position.y, gameObject.transform.position.z);

                if(exited()){
                    state = CustomerState.entry;
                    init(MainManager.Instance.next());

                    dialogBox.SetActive(false);
                }

                break;
        }
    }

    public bool arrived(){
        return gameObject.transform.position.x <= 0;
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
        this.data = data;
    }

    public string feedback(Meal m){
        if(data.preferences == null){ //How is this even possible
            Debug.LogWarning("I don't know how this is even possible, but " + data.customerName + " needs to be re-initialized?????");
            data.initPreferences();
        }

        int len = data.preferences.Length;
        bool[] complete = new bool[len];
        bool pass = false;
        for(int i = 0; i < len; i++){
            bool passPref = data.preferences[i].valid(m);
            if(passPref) pass = true;
            if(!passPref && data.preferences[i] as NoLunchlyPreference != null){
                return data.passText(i);
            }
            complete[i] = passPref;
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

    public bool passes(Meal m){
        int len = data.preferences.Length;
        for(int i = 0; i < len; i++){
            bool passPref = data.preferences[i].valid(m);
            if(!passPref) return false;
        }
        return true;
    }

    public CustomerData getData(){
        return data;
    }

    public enum CustomerState{
        entry, wait, served, exit
    }
}