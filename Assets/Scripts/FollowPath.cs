using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public float speed = 5;
    public Transform path = null;
    int child_number = 0;
    Vector3 target;
    int nChilds;

    void Start() {
        if(path) {
            nChilds = path.childCount - 1;
            if (!IsSpeedPositive()){
                child_number = nChilds;
            }
            findTarget();
        }
    }


    void Update() {
        if(path) {
            float deltaDistance = Time.deltaTime * Mathf.Abs(speed);
            transform.position =
                Vector3.MoveTowards(transform.position,
                                    target,
                                    deltaDistance);
            if (transform.position == target){
                findTarget();
            }
        }
    }

    
    void findTarget(){
        target = path.GetChild(child_number).transform.position;
        if (IsSpeedPositive()){
            FindPositiveChild();
        }
        else {
            FindNegativeChild();
        }  
    }
   
    void FindPositiveChild(){
        if(child_number >= nChilds){
            child_number = 0;
        }
        else{
            child_number++;
        }
    }
    
    void FindNegativeChild(){
        if(child_number <= 0){
            child_number = nChilds;
        }
        else{
            child_number--;
        }
    }

    bool IsSpeedPositive(){
        return speed > 0;
    }

    public List<Transform> GetAllChildsPath() {
        List<Transform> listOfChilds = new List<Transform>();
        
        if(path == null) {
            return null;
        }

        foreach (Transform child in path) {
              listOfChilds.Add(child);
        }

        return listOfChilds;
    }

    public void DeleteOneChildOfPath(int index) {
        List<Transform> listOfChilds = new List<Transform>();
        listOfChilds = GetAllChildsPath();
        Object.DestroyImmediate(listOfChilds[index].gameObject);
    }
}
