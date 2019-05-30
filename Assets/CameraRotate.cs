using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{   
    public float speed;
    public float prueba;
    
    void Start() {
        
    }
    void Update() {
        prueba = Input.mouseScrollDelta.y;
        transform.Rotate(0, 0, Input.mouseScrollDelta.y * speed);
    }
}
