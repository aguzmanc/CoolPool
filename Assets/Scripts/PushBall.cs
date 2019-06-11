using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    public float strength = 30;
    public float cooldown = 5;
    float cd;
    Rigidbody ballRigidbody;
    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        CooldownTimer();
    }

    void OnCollisionEnter(Collision other)
    {   Transform c = other.gameObject.transform;
        if (c.GetComponent<FollowTarget>() && cd <= 0) {
            Vector3 direction = transform.position - c.parent.transform.position;
            direction.y = 0;
            Vector3 position = other.GetContact(0).point;
            position.y = 0;
            ballRigidbody.AddForceAtPosition(direction.normalized * strength, position, ForceMode.Impulse);
            cd = cooldown;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Goal>()) {
           GameManager.TriggerVictory();
        }
    }
    
    void CooldownTimer() {
        if (cd > 0)
            cd = cd - (1 * Time.deltaTime);
    }
}
