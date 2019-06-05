using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    public float strength = 30;
    Rigidbody ballRigidbody;
    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision other)
    {   Transform c = other.gameObject.transform;
        if (c.GetComponent<FollowTarget>()) {
            Vector3 direction = transform.position - c.parent.transform.position;
            direction.y = 0;
            Vector3 position = other.GetContact(0).point;
            position.y = 0;
            ballRigidbody.AddForceAtPosition(direction.normalized * strength, position, ForceMode.Impulse);
        }
    }
}
