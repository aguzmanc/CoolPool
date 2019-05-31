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
    

    void Start()
    {  
        resetHookPropierties();
        resetPosition();
        rangeOfHook = 20;
    }
    
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            isMovement = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if(isHooked) {
                isHooked = false;
                resetHookPropierties();
                resetPosition();
            }
            else {
                startMovement();
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
}