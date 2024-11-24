using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour, IToggleable
{
    public static CameraManager Instance;

    public bool down = false;
    public float panSpeed = 12f;

    public void toggle(bool state){
        if(state == down) return;
        down = state;
    }

    private void Awake() {
        if(Instance != null) Debug.LogError("Too lazy to write a message");
        Instance = this;
    }

    private void Update() {
        UnityEngine.Vector3 pos = gameObject.transform.position;
        pos.y -= panSpeed * Time.deltaTime * (down ? 1 : -1);
        pos.y = math.clamp(pos.y, -12.75f, 1.4f);
        gameObject.transform.position = pos;
    }

    public bool returned(){
        return gameObject.transform.position.y >= 1.4f;
    }
}
