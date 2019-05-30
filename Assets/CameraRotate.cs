using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{   
    public float speed;
    
    void Start() {
        
    }

    void Update() {
        transform.Rotate(0, Input.mouseScrollDelta.y * speed, 0, 0);
    }
}
