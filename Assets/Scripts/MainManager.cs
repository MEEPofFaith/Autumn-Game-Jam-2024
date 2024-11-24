using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Meal currentMeal = new Meal();

    public Dictionary<string, int> served = new Dictionary<string, int>();

    private int test = 0;
    public float testDelay = 1;

    private void Awake() {
        if(Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Data.customers[0].type = CustomerData.CustomerType.normal;
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

    public CustomerData next(){
        for(int i = 0; i < Data.customers.Count; i++){
            test = (test + 1) % Data.customers.Count;
            if(Data.customers[test].normal != null) return Data.customers[test];
        }
        return Data.customers[test];
    }
}
