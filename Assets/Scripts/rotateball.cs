using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody ballRigidbody;
    public float speed;
    public Transform cubePosition;
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
        
            Vector3 direction = cubePosition.position - transform.position;
            direction.y = 0.0f;
            direction = direction.normalized;
            ballRigidbody.AddForce(direction * speed, ForceMode.Impulse);
        }
    }
}
