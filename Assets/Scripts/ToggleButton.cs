using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    
    public bool state = true;
    public GameObject[] toggles;

    private void OnMouseDown() {
        foreach(GameObject toggle in toggles){
            toggle.GetComponent<IToggleable>().toggle(state);
        }
    }
}
