public class IngredientPreference : IPreferences{
    public string requested = "";
    public int minRequired = 1;

    public bool valid(Meal m){
        int counted = 0;
        foreach(IngredientData ingredient in m.ingredients){
            if(ingredient.itemName == requested) counted++;
        }
        return counted >= minRequired;
    }
}