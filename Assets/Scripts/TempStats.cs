using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempStats : MonoBehaviour
{
    public static TempStats Instance;

    public TMP_Text text;

    private void Awake() {
        if(Instance != null) Debug.LogError("WHat");

        Instance = this;
        text = gameObject.GetComponent<TMP_Text>();
    }
    
    public void updateText(){
        float[] stats = MainManager.Instance.currentMeal.getStats();

        text.SetText(
            "Smell: " + stats[(int)IngredientData.IngredientStat.smell]
            + "\nFlavor: " + stats[(int)IngredientData.IngredientStat.flavor]
            + "\nAppearance: " + stats[(int)IngredientData.IngredientStat.appearance]
            + "\nTexture: " + stats[(int)IngredientData.IngredientStat.texture]
        );
    }
}
