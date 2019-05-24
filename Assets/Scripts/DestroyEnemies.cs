using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision c) {
        if (c.gameObject.name == "Capsule")
            Destroy(c.gameObject);
        
    }
}
