using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour {
    public float strength = 30;
    Rigidbody ballRigidbody;
    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Goal>()) {
           GameManager.TriggerVictory();
        }
    }
    
}
