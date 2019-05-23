using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public LineRenderer hookLineRenderer;
    public CapsuleCollider capsuleColliderHook;
    public float speed;

    void Start()
    {
        
    }
    
    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            float initialPosition = hook.GetPosition(1).z;
            // transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
            // capsuleColliderHook.center = new Vector3(0.0f, 0.0f, capsuleColliderHook.center.z + 10.0f);
            // capsuleColliderHook.center = new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
            
            print(speed * Time.deltaTime);
            hook.SetPosition(1, new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
        }
        // hook.SetPosition(1, new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
    }

    // void OnTriggerEnter(Collider c) {
    //     if(c.name == "Wall") {
    //         print("Muro impactado");
    //         hook.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
    //         capsuleColliderHook.height = 0.0f;
    //     }
    //     else {
    //         print(c);
    //         // hook.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
    //         // capsuleColliderHook.height = 0.0f;
    //     }
    // }
}
