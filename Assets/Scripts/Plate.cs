using System;
using Unity.Mathematics;
using UnityEngine;

public class Plate : MonoBehaviour, IToggleable
{
    public static Plate Instance;

    public bool down = true;
    public float moveSpeed = 12f;

    private void Awake() {
        Instance = this;
    }

    public void toggle(bool state){
        if(state == down) return;
        down = state;

        if(down){
            Vector3 pos = gameObject.transform.localPosition;
            pos.y = -3.05f;
            gameObject.transform.localPosition = pos;
        }
    }

    private void Update() {
        if(!down){
            Vector3 pos = gameObject.transform.localPosition;
            pos.y += moveSpeed * Time.deltaTime;
            pos.y = math.min(pos.y, 28f);
            gameObject.transform.localPosition = pos;
        }
    }

    public bool arrived(){
        return gameObject.transform.localPosition.y >= 28f;
    }
}