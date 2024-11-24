using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngRemoveButton : MonoBehaviour
{
    public IngredientData ingredient;

    private void OnMouseDown() {
        MainManager.Instance.removeIngredient(ingredient);
    }
}
