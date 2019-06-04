using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    public float speed = 30;
    Rigidbody ballRigidbody;
   
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit)) {
            if (hit.transform.gameObject.GetComponent<PushBall>()) {
                Vector3 direction = hit.point - Camera.main.transform.position;
                direction.y = 0.0f;
                direction = direction.normalized;
                ballRigidbody.AddForceAtPosition(direction * speed, hit.point);
                
            }
        }
    }


    void OnCollisionEnter(Collision other)
    {   Transform c = other.gameObject.transform;
        if (c.name == "Objetivo") {
            Vector3 direction = transform.position - c.parent.transform.position;
            Vector3 position = other.GetContact(0).point;
            //ballRigidbody.AddForce(direction, position, 1000, ForceMode.Impulse);
            ballRigidbody.AddForceAtPosition(direction.normalized * speed, position);
        }
        

    }
}
