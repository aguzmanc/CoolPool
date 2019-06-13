using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MoveGameObjects))]
public class MoveGameObjectsEditor : Editor {
    
    MoveGameObjects Target { get => (MoveGameObjects) target; }
    static List<Transform> movableObjects;
    static Dictionary<string, bool> buttonsState;
    
    public void OnEnable() {
        movableObjects = new List<Transform>();
        buttonsState = new Dictionary<string, bool>();

        ChargeAllChilds(Target.transform);
        chargeDictionary();
    }

    void chargeDictionary() {
        for (int i = 0 ; i < movableObjects.Count; i++) {
            buttonsState.Add(movableObjects[i].name, false);
        }
    }

    void chargeButtons() {
        for (int i = 0 ; i < movableObjects.Count; i++) {
            buttonsState[movableObjects[i].name] = GUILayout.Toggle(buttonsState[movableObjects[i].name], "Mover " + movableObjects[i].name, "Button");
        }
    }

    Transform findTransformInList(string nameObject) {
        foreach(Transform tranform in movableObjects) {
            if(tranform.name == nameObject) { 
                return tranform;
            }
        }
        return null;
    }
    
    public void OnDisable() {
        movableObjects = null;
        buttonsState = null;
    }

    void OnSceneGUI() {
        for (int i = 0 ; i < movableObjects.Count; i++) {
            if(buttonsState[movableObjects[i].name]) {
                movableObjects[i].position = Handles.PositionHandle(movableObjects[i].position, Quaternion.identity);
            }
        }
        SceneView.RepaintAll();
    }
    
    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        chargeButtons();
    }


    void ChargeAllChilds(Transform transform) {    
        for (int i= 0; i < transform.childCount; i++){
            ChargeAllChilds(transform.GetChild(i));
        }

        if (transform.GetComponent<IMoveObjects>() != null){
            movableObjects.Add(transform);
        }
    }
}