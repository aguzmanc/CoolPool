using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    
    public GameObject hook;
    LineRenderer hookLineRenderer;

    void Start()
    {
        hookLineRenderer = hook.gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.GetComponent<Wall>()) {
            hook.GetComponent<Hook>().resetHookPropierties();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(isBlockCube(collision)) {
            hook.GetComponent<Hook>().stopMovement();
            hook.GetComponent<Hook>().hookedWithBlockCube();
            hookLineRenderer.SetPosition(1, 
                                        hookLineRenderer.transform.InverseTransformPoint(
                                        collision.contacts[0].point));
            
            transform.parent.GetComponent<Hook>().setTargetCollision(collision.transform);
        }
        if (collision.transform.GetComponent<PushBall>()) {
            Vector3 direction = collision.transform.position - transform.parent.position;
            direction.y = 0;
            Vector3 position = collision.GetContact(0).point;
            position.y = 0;
            collision.transform.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * 30, position, ForceMode.Impulse);
        }
    }
    
    bool isBlockCube(Collision collision) {
        return collision.gameObject.GetComponent<BlockCube>();
    }
    
    bool isBall(Collision collision) {
        return collision.gameObject.GetComponent<PushBall>();
    }
}