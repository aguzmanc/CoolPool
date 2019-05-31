using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public LineRenderer hookLineRenderer;
    public float speed;
    float zFinalPosition;
    bool isMovement;
    float rangeOfHook;
    bool isHooked;
    public Transform targetHooked;

    void Start()
    {  
        resetHookPropierties();
        resetPosition();
        rangeOfHook = 20;
    }
    void resetHookBehavior() {
        isHooked = false;
        targetHooked = null;
        resetHookPropierties();
        resetPosition();
    }
    void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) {
            isMovement = true;
        }
        
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if(isHooked) {
                resetHookBehavior();
            }
            else {
                startMovement();
            }
        }

        if(Input.GetKey(KeyCode.Q) && targetHooked) {
            float deltaDistance = Time.deltaTime * speed;
            Transform playerTransform = transform.parent;
            
            
            
            if(hookLineRenderer.GetPosition(1).z > 0.9f) {
                playerTransform.position =
                Vector3.MoveTowards(playerTransform.position,
                                    hookLineRenderer.transform.TransformPoint(hookLineRenderer.GetPosition(1)),
                                    deltaDistance);
                zFinalPosition-= speed * Time.deltaTime;
                hookLineRenderer.SetPosition(1, new Vector3(0, 0, zFinalPosition));
            }
        }

        if(isMovement) {
            hookLineRenderer.SetPosition(1, new Vector3(0, 0, zFinalPosition));
            zFinalPosition+= speed * Time.deltaTime;
        }
        
        if(isOutRange()) {
            resetHookPropierties();
            resetPosition();
        }
    }
    
    public void resetPosition() {
        hookLineRenderer.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
    }

    public void stopMovement() {
        isMovement = false;
    }
    
    public void startMovement() {
        isMovement = true;
    }

    public void resetHookPropierties() {
        zFinalPosition = 0;
        isMovement = false;
    }

    bool isOutRange() {
        return (hookLineRenderer.GetPosition(1).z >= rangeOfHook);
    }

    public void hookedWithBlockCube() {
        isHooked = true;
    }

    public void setTargetCollision(Transform targetTransform) {
        targetHooked = targetTransform;
    }

    void OnTriggerEnter(Collider collider) {
        // if(collider.gameObject.GetComponent<BlockCube>()) {
        //     resetHookBehavior();
        // }
    }
}