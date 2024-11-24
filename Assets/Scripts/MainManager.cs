using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Meal currentMeal = new Meal();

    public Dictionary<string, int> served = new Dictionary<string, int>();

    private int test = 0;
    private float count = 5;
    public float testDelay = 1;

    private void Awake() {
        if(Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Customer.Instance.init(Data.customers[0]);
        updateDisplay();
    }

    public bool canEditMeal(){
        return CameraManager.Instance.down && !Pot.Instance.up;
    }

    public void addIngredient(IngredientData data){
        if(!canEditMeal()) return;

        if(data.type == IngredientData.IngredientType.ingredient){ //If ingredient, add until full
            if(currentMeal.ingredients.Count < 5){
                currentMeal.ingredients.Add(data);
            }
        }else{
            if(currentMeal.seasoning == data){ //If seasoning, toggle/swap
                currentMeal.seasoning = null;
            }else{
                currentMeal.seasoning = data;
            }
        }
        updateDisplay();
    }

    public void removeIngredient(IngredientData data){
        if(!canEditMeal()) return;

        if(data.type == IngredientData.IngredientType.ingredient){
            currentMeal.ingredients.Remove(data);
        }
        updateDisplay();
    }

    public void clearIngredients(){
        currentMeal.ingredients.Clear();
        currentMeal.seasoning = null;
        updateDisplay();
    }

    public void updateDisplay(){
        foreach(IngredientDisplay disp in IngredientDisplay.Instances){
            disp.updateDisplay();
        }
    }

    private void Update() {
        if(Customer.Instance.state == Customer.CustomerState.wait && Customer.Instance.arrived()){
            count -= Time.deltaTime;
            if(count < 0){
                count = testDelay;
                Customer.Instance.state = Customer.CustomerState.exit;
            }
        }else if(Customer.Instance.state == Customer.CustomerState.exit && Customer.Instance.exited()){
            test = next();
            Data.customers[test].setType((CustomerData.CustomerType)Random.Range(0, 3));
            Customer.Instance.init(Data.customers[test]);
        }
    }

    private int next(){
        for(int i = 0; i < Data.customers.Count; i++){
            test = (test + 1) % Data.customers.Count;
            if(Data.customers[test].normal != null) return test;
        }
        return test;
    }
}
