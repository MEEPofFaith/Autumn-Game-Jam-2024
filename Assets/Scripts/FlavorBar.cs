using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorBar : MonoBehaviour
{
    public static List<FlavorBar> Instances = new List<FlavorBar>();

    public Sprite fullStar, halfStar;
    public IngredientData.IngredientStat stat;
    public SpriteRenderer[] stars;

    private void Awake() {
        Instances.Add(this);
    }

    public void updateStars(){
        float mealStat = MainManager.Instance.currentMeal.getStats()[(int)stat];
        for(int i = 0; i < 5; i++){
            if(mealStat >= i){
                stars[i].sprite = fullStar;
            }else if(mealStat >= i - 0.5f){
                stars[i].sprite = halfStar;
            }else{
                stars[i].sprite = null;
            }
        }
    }
}
