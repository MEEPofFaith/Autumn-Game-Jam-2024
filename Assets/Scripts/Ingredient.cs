using UnityEngine;

public class Ingredient : MonoBehaviour {
    public IngredientData data;

    private void Awake() {
    }

    private void OnMouseDown() {
        MainManager.Instance.addIngredient(data);
    }
}