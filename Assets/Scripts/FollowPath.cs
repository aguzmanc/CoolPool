using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public float speed = 5;
    Transform path;
    int pivote = 1;
    Vector3 target, origin, target2, target3;

    void Start()
    {
        path = transform.GetChild(0);
        //nChilds = path.transform.childCount;
        //target = path.transform.GetChild(0).transform.position;
        target2 = path.transform.GetChild(1).transform.position;
        origin = transform.position;
    }
    void Update()
    {
        float deltaDistance = Time.deltaTime * speed;
        transform.position =
            Vector3.MoveTowards(transform.position,
                                target,
                                deltaDistance);
       
        if (transform.position == target){
            switch (pivote)
            {
            case 0:
                findTarget();
                pivote++;
                break;
               
            case 1:
                target = target2;
                pivote++;
                break;
            default:
                target = origin;
                pivote = 0;
                break;
            }
        }

    }
    void findTarget(){
        target = path.transform.GetChild(pivote).transform.position;
    }
}
