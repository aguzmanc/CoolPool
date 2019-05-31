using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public LineRenderer hookLineRenderer;
    public float speed;
    float zFinalPosition;
    bool isLineHookDrawing;
    float rangeOfHook;
    bool isHooked;
    public Transform targetHooked;
    
    Ray ray;
    RaycastHit hit;
    float xPositionRayCast;
    Transform playerTransform;

    void Start()
    {  
        resetHookPropierties();
        resetPosition();
        rangeOfHook = 20;
        playerTransform = transform.parent;
    }

    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            startLineHookDrawing();
        }
        
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if(isHooked) {
                resetHookBehavior();
            }
            else {
                startLineHookDrawing();
            }
        }

        if(Input.GetKey(KeyCode.Q) && targetHooked) {
            if(isHookCanBeMoreLittle()) {
                movePlayerTowardsHookedPoint();
            }
        }

        if(isLineHookDrawing) {
            drawHookLine();
        }
        
        if(isOutRange()) {
            resetHookPropierties();
            resetPosition();
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) &&
            Physics.Raycast(ray, out hit) && !isLineHookDrawing) {
            if(isHooked) {
                resetHookBehavior();
            }
            else {
                changeRotationPlayerToHitPoint();
                startLineHookDrawing();
            }
        }
    }

    void resetHookBehavior() {
        isHooked = false;
        targetHooked = null;
        resetHookPropierties();
        resetPosition();
    }
    
    public void resetPosition() {
        hookLineRenderer.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
    }
    
    void startLineHookDrawing() {
        isLineHookDrawing = true;
    }

    public void resetHookPropierties() {
        zFinalPosition = 0;
        isLineHookDrawing = false;
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

    public void stopMovement() {
        isLineHookDrawing = false;
    }

    bool isHookCanBeMoreLittle() {
        return hookLineRenderer.GetPosition(1).z > 0.9f;
    }

    void movePlayerTowardsHookedPoint() {
        float deltaDistance = Time.deltaTime * speed;
        playerTransform.position =
            Vector3.MoveTowards(playerTransform.position,
                                hookLineRenderer.transform.TransformPoint(hookLineRenderer.GetPosition(1)),
                                deltaDistance);
            zFinalPosition-= speed * Time.deltaTime;
            hookLineRenderer.SetPosition(1, new Vector3(0, 0, zFinalPosition));
    }

    void changeRotationPlayerToHitPoint() {
        playerTransform.LookAt(hit.point);
        playerTransform.eulerAngles = new Vector3(0, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);
    }
    
    void drawHookLine() {
        hookLineRenderer.SetPosition(1, new Vector3(0, 0, zFinalPosition));
        zFinalPosition+= speed * Time.deltaTime;
    }
}