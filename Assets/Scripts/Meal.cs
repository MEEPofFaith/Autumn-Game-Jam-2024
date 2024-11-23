using System.Collections.Generic;
using System.Numerics;
using static IngredientData;

public class Meal{
    public List<IngredientData> ingredients;

    public float[] getStats(){
        float[] stats = new float[System.Enum.GetValues(typeof(IngredientStat)).Length];
        int[] counts = new int[stats.Length];

        foreach(IngredientData ing in ingredients){
            if(ing.type == IngredientType.ingredient || ing.smell >= 0){ //Ingredient -> Always add | Seasoning -> Only add if has value
                stats[(int)IngredientStat.smell] += ing.smell;
                counts[(int)IngredientStat.smell]++;
            }
            if(ing.type == IngredientType.ingredient || ing.flavor >= 0){
                stats[(int)IngredientStat.flavor] += ing.flavor;
                counts[(int)IngredientStat.flavor]++;
            }
            if(ing.type == IngredientType.ingredient || ing.appearance >= 0){
                stats[(int)IngredientStat.appearance] += ing.appearance;
                counts[(int)IngredientStat.appearance]++;
            }
            if(ing.type == IngredientType.ingredient || ing.smell >= 0){
                stats[(int)IngredientStat.texture] += ing.texture;
                counts[(int)IngredientStat.texture]++;
            }
        }

        for(int i = 0; i < stats.Length; i++){
            stats[i] /= counts[i]; //Average
        }

        return stats;
    }
}