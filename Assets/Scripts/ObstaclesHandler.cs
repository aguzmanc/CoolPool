using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstaclesHandler : MonoBehaviour {
    // ésto de aquí debería ser una propiedad de algún script en el obstáculo...
    public Vector3 scalePlaceholder; 
    public GameObject prototype;
    public GameObject CreateObstacle(Vector3 pos) {
        GameObject created = Instantiate(prototype);
        created.transform.position = pos;
        created.transform.parent = transform.parent;
        return created;
    }
    public GameObject GetPrefabReference(){
        return prototype;
    }
    
}
