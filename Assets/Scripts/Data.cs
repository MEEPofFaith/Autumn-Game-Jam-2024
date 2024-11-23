using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Data : MonoBehaviour {
    private static bool init = false;
    public static List<IngredientData> ingredients = new List<IngredientData>();
    public static List<IngredientData> seasonings = new List<IngredientData>();
    public static List<CustomerData> customers = new List<CustomerData>();

    private void Awake() {
        DontDestroyOnLoad(gameObject);

        if(init) return;
        init = true;

        ingredients = Resources.LoadAll<IngredientData>("Ingredients/").ToList<IngredientData>();
        seasonings = Resources.LoadAll<IngredientData>("Seasonings/").ToList<IngredientData>();
        customers = Resources.LoadAll<CustomerData>("Customers/").ToList<CustomerData>();

        foreach(CustomerData cus in customers){
            cus.initPreferences();
        }
    }
}