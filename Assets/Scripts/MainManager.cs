using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static CustomerData;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Meal currentMeal = new Meal();

    public Dictionary<string, int> served = new Dictionary<string, int>();
    public int totalScore = 0;
    //Minimum number of perfect scores before ants/parasites of that customer begin to spawn.
    public int minMod = 2;

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

    public void serveMeal(){
        Customer c = Customer.Instance;
        totalScore += c.score(currentMeal);
        if(c.passes(currentMeal)){
            string key = c.getData().key();
            if(!served.ContainsKey(key)){
                served.Add(key, 1);
            }else{
                served[key]++;
            }
        }

        clearIngredients();
    }

    public CustomerData next(){
        CustomerData cust = Data.randomCustomer();
        CustomerType type = CustomerType.normal;

        string key = cust.key();
        if(served.ContainsKey(key) && served[key] > minMod){ //Begin modifying after minMod perfect scores.
            float rand = Random.Range(0, 1);
            if(rand < 0.1f){ //10% chance for parasite
                type = CustomerData.CustomerType.parasite;
            }else if(rand < 0.35f){ //25% chance for ants
                type = CustomerData.CustomerType.ants;
            }
        }

        cust.setType(type);

        return cust;
    }
}
