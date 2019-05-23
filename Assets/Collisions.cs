using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    
    public GameObject hook;
    LineRenderer hookLine;


    void Start()
    {
        hookLine = hook.gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {

        if(collider.gameObject.GetComponent<Wall>()) {
            hookLine.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
            print(collider.gameObject.name);
        }
    }
}
