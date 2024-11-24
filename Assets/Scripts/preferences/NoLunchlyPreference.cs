public class NoLunchlyPreference : IPreferences{
    public bool valid(Meal m){
        foreach(IngredientData ingredient in m.ingredients){
            if(ingredient.itemName == "Lunchly") return false;
        }
        return true;
    }
}