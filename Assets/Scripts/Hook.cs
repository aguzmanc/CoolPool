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
    
    void Start()
    {  
        resetPropertiesLineRendererHook();
        resetPosition();
        rangeOfHook = 10;
    }
    
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            isMovement = true;
        }

        if(isMovement) {
            hookLineRenderer.SetPosition(1, new Vector3(0, 0, zFinalPosition + speed * Time.deltaTime));
            zFinalPosition+= 0.1f;
        }
        
        if(isOutRange()) {
            resetPosition();
            resetPropertiesLineRendererHook();
        }
    }
    
    void resetPosition() {
        zFinalPosition = 0;
        hookLineRenderer.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
    }

    void resetMovent() {
        isMovement = false;
    }
    
    void resetPropertiesLineRendererHook() {
        zFinalPosition = 0;
        isMovement = false;
    }

    bool isOutRange() {
        return (hookLineRenderer.GetPosition(1).z >= rangeOfHook);
    }
}

