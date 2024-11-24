using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Ingredient", order = 1)]
public class IngredientData : ScriptableObject{
    public string itemName = "Ingredient";
    public string flavorText = "";

    public IngredientType type = IngredientType.ingredient;

    public int smell; //fragrant - pungent
    public int flavor; // ???
    public int appearance; // "appealing" - disgusting
    public int texture; // slimy - gravely

    public Sprite sprite;
    public Sprite itemSprite;

    public enum IngredientType{
        ingredient, seasoning
    }

    public enum IngredientStat{
        smell,
        flavor,
        appearance,
        texture
    }
}