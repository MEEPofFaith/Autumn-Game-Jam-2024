using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Ingredient", order = 1)]
public class IngredientData : ScriptableObject{
    public int smell;
    public int flavor;
    public int appearance;
    public int texture; //mmmm cronchy

    public string itemName = "Ingredient";
    public string flavorText = "";

    public Sprite sprite;
}