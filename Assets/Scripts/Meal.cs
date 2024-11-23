using System.Collections.Generic;
using System.Numerics;
using static IngredientData;

public class Meal{
    public List<IngredientData> ingredients;

    public int[] getStats(){
        int[] stats = new int[System.Enum.GetValues(typeof(IngredientStat)).Length];

        foreach(IngredientData ingredient in ingredients){
            stats[(int)IngredientStat.smell] += ingredient.smell;
            stats[(int)IngredientStat.flavor] += ingredient.flavor;
            stats[(int)IngredientStat.appearance] += ingredient.appearance;
            stats[(int)IngredientStat.texture] += ingredient.texture;
        }

        return stats;
    }
}