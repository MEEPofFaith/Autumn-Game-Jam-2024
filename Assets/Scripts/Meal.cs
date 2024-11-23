using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using static IngredientData;

public class Meal{
    public List<IngredientData> ingredients;

    public float[] getStats(){
        float[] stats = new float[System.Enum.GetValues(typeof(IngredientStat)).Length];

        foreach(IngredientData ing in ingredients){
            stats[(int)IngredientStat.smell] += ing.smell;
            stats[(int)IngredientStat.flavor] += ing.flavor;
            stats[(int)IngredientStat.appearance] += ing.appearance;
            stats[(int)IngredientStat.texture] += ing.texture;
        }

        for(int i = 0; i < stats.Length; i++){
            stats[i] = math.clamp(stats[i] / 5, 0, 5);
        }

        return stats;
    }
}