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

    private void Awake() {
        Instance = this;
    }

    public void toggle(bool state){
        if(state == up) return;
        up = state;

        if(state){
            Vector3 pos = spork.transform.localPosition;
            pos.y = 35;
            spork.transform.localPosition = pos;

            foreach(FlavorBar bar in FlavorBar.Instances){
                bar.updateStars();
            }
        }
    }

    // Update is called once per frame
    void Update(){
        Vector3 pos = gameObject.transform.position;
        pos.y += moveSpeed * Time.deltaTime * (up ? 1 : -1);
        pos.y = math.clamp(pos.y, -23.5f, -16.9f);
        gameObject.transform.position = pos;

        pos = spork.transform.localPosition;
        if(up){
            pos.y -= moveSpeed * 2 * Time.deltaTime * (up ? 1 : -1);
            pos.y = math.clamp(pos.y, 4f, 35f);
        }

        pos.x = (float)(math.sin(Time.time / stirInterval * math.PI_DBL) * 3f);

        spork.transform.localPosition = pos;
    }
}
