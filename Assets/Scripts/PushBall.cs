using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    public float speed = 300;
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
}
