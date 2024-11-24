using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pot : MonoBehaviour, IToggleable
{
    public static Pot Instance;

    public bool up = false;
    public float stirInterval = 1f;
    public float moveSpeed = 12f;

    public GameObject spork;
    public GameObject meal;

    private void Awake() {
        Instance = this;
    }

    public void toggle(bool state){
        if(state == up) return;
        up = state;

        if(state){
            Vector3 pos = gameObject.transform.position;
            pos.y = -23.5f;
            gameObject.transform.position = pos;

            pos = spork.transform.localPosition;
            pos.y = 42;
            spork.transform.localPosition = pos;

            foreach(FlavorBar bar in FlavorBar.Instances){
                bar.updateStars();
            }
        }
    }

    // Update is called once per frame
    void Update(){
        Vector3 sPos = spork.transform.localPosition;
        if(up){
            Vector3 pos = gameObject.transform.position;
            pos.y += moveSpeed * Time.deltaTime;
            pos.y = math.min(pos.y, -16.9f);
            gameObject.transform.position = pos;

            sPos.y -= moveSpeed * 2 * Time.deltaTime;
            sPos.y = math.max(sPos.y, 4f);
        }

        sPos.x = (float)(math.sin(Time.time / stirInterval * math.PI_DBL) * 3f);

        spork.transform.localPosition = sPos;
    }
}
