﻿using System.Collections;
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
    
    AudioSource effectsSounds;
    public AudioClip hookSound;
    public AudioClip movePlayerSound;

    public AudioClip hookImpactBlockSound;
    public Transform Player { get => playerTransform; }

    bool isMovePlayerSoundActivate;

    Ray ray;
    RaycastHit hit;
    float xPositionRayCast;
    Transform playerTransform;
    
    

    void Start() {  
        resetHookPropierties();
        rangeOfHook = 20;
        playerTransform = transform.parent;
        effectsSounds = GetComponent<AudioSource>();
    }

    void Update() {
        updatePlayerControls();
        updateHookControl();
    } 

    void updateHookControl() {
        if(isLineHookDrawing) {
            drawHookLine();
        }
        
        if(isOutRange()) {
            resetHookBehavior();
        }
    }

    void updatePlayerControls() {

        if(Input.GetKey(KeyCode.Q) && targetHooked) {
            
            if(isHookCanBeMoreLittle()) {
                movePlayerTowardsHookedPoint();
                ActivateMovePlayerSound();
            }
        }
        else {
            DeactivateMovePlayerSound();
        }
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) &&
            Physics.Raycast(ray, out hit) && 
            !isLineHookDrawing) {
            
            if(isHooked) {
                resetHookBehavior();
            }

            else {
                ActivateHookSound();
                changeRotationPlayerToHitPoint();
                startLineHookDrawing();
            }
        }
    }

    void resetHookBehavior() {
        resetHookPropierties();
    }
    
    void startLineHookDrawing() {
        isLineHookDrawing = true;
    }

    public void resetHookPropierties() {
        zFinalPosition = 0;
        isLineHookDrawing = false;
        isHooked = false;
        targetHooked = null;
        hookLineRenderer.SetPosition(1, new Vector3(0.0f, 0.0f, 0.0f));
    }

    bool isOutRange() {
        return (hookLineRenderer.GetPosition(1).z >= rangeOfHook);
    }

    public void hookedWithBlockCube() {
        isHooked = true;
        ActivateHookImpactBlockSound();
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
    
    void ActivateHookSound() {
        effectsSounds.clip = hookSound;
        effectsSounds.Play();
    }
    
    void ActivateMovePlayerSound() {
        if(isMovePlayerSoundActivate == false) {
            effectsSounds.clip = movePlayerSound;
            isMovePlayerSoundActivate = true;
            effectsSounds.Play();
            effectsSounds.loop = true;
        }
    }

    void DeactivateMovePlayerSound() {
        isMovePlayerSoundActivate = false;
        effectsSounds.loop = false;
    }

    void ActivateHookImpactBlockSound() {
        effectsSounds.clip = hookImpactBlockSound;
        effectsSounds.Play();
    }
}
