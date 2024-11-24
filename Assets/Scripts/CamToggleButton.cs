using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToggleButton : MonoBehaviour
{
    
    public bool down = true;

    private void OnMouseDown() {
        CameraManager.Instance.down = down;
    }
}
