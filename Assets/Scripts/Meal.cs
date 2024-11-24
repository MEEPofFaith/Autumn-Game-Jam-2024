using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using static IngredientData;

public class Meal{
    public List<IngredientData> ingredients = new List<IngredientData>();
    public IngredientData seasoning;

    public float[] getStats(){
        float[] stats = new float[System.Enum.GetValues(typeof(IngredientStat)).Length];

        foreach(IngredientData ing in ingredients){
            stats[(int)IngredientStat.smell] += ing.smell;
            stats[(int)IngredientStat.flavor] += ing.flavor;
            stats[(int)IngredientStat.appearance] += ing.appearance;
            stats[(int)IngredientStat.texture] += ing.texture;
        }

        if(seasoning != null){
            stats[(int)IngredientStat.smell] += seasoning.smell;
            stats[(int)IngredientStat.flavor] += seasoning.flavor;
            stats[(int)IngredientStat.appearance] += seasoning.appearance;
            stats[(int)IngredientStat.texture] += seasoning.texture;
        }

        for(int i = 0; i < stats.Length; i++){
            stats[i] = math.clamp(stats[i] / ingredients.Count, 0, 5);
        }

        return stats;
    }
}