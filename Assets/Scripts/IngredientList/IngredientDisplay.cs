using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDisplay : MonoBehaviour
{
    public static List<IngredientDisplay> Instances = new List<IngredientDisplay>();

    public Sprite[] numSprites;
    public SpriteRenderer[] numRenders;
    public SpriteRenderer[] itemRenders;
    public GameObject[] delButtons;


    private void Awake() {
        Instances.Add(this);
    }

    // Start is called before the first frame update
    public void updateDisplay(){
        Meal m = MainManager.Instance.currentMeal;
        Dictionary<string, int> items = new Dictionary<string, int>();
        foreach(IngredientData data in m.ingredients){
            if(items.ContainsKey(data.itemName)){
                items[data.itemName]++;
            }else{
                items.Add(data.itemName, 0);
            }
        }

        int item = 0;
        foreach(IngredientData data in Data.ingredients){
            if(items.ContainsKey(data.itemName)){
                int count = items[data.itemName];
                numRenders[item].sprite = numSprites[count];
                itemRenders[item].sprite = data.itemSprite;
                delButtons[item].SetActive(true);
                delButtons[item].GetComponent<IngRemoveButton>().ingredient = data;
                item++;
            }
        }

        for(int i = item; i < 5; i++){
            numRenders[i].sprite = null;
            itemRenders[i].sprite = null;
            delButtons[i].SetActive(false);
        }
    }
}
