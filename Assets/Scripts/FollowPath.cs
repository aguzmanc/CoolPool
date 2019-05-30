﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public float speed = 5;
    public Transform path;
    int child_number = 0;
    Vector3 target;
    int nChilds;

    void Start()
    {
        nChilds = path.childCount -1;
        findTarget();
    }


    void Update()
    {
        float deltaDistance = Time.deltaTime * speed;
        transform.position =
            Vector3.MoveTowards(transform.position,
                                target,
                                deltaDistance);
        if (transform.position == target){
            findTarget();
        }
    }

    
    void findTarget(){
        target = path.GetChild(child_number).transform.position;
        if(child_number >= nChilds){
                child_number = 0;
        }
        else{
            child_number++;
        }
    }
}