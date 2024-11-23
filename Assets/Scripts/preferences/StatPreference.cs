using static IngredientData;

public class StatPreference : IPreferences{
    public IngredientStat stat;
    public int min, max;

    public bool valid(Meal m){
        int current = m.getStats()[(int)stat];
        return current >= min && current <= max;
    }
}