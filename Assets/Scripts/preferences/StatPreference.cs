using static IngredientData;

public class StatPreference : IPreferences{
    public IngredientStat stat;
    public float min, max;

    public bool valid(Meal m){
        float current = m.getStats()[(int)stat];
        return current >= min && current <= max;
    }
}