using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    public float speed = 10;
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
            if (hit.transform.name == "Sphere" ) {
                Vector3 direction = hit.normal;
                direction.y = 0.0f;
                ballRigidbody.AddForce(-direction * speed, ForceMode.Impulse);
            }
        }
    }
}
