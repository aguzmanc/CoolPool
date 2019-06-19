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

    bool chargeButtons() {
        bool changed = false;
        for (int i = 0 ; i < movableObjects.Count; i++) {
            bool newValue = GUILayout.Toggle(buttonsState[movableObjects[i].name], "Mover " + movableObjects[i].name, "Button");
            if (newValue != buttonsState[movableObjects[i].name]) {
                changed = true;
            }
            buttonsState[movableObjects[i].name] = newValue;
        }
        return true;
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

    void DrawGizmos() {
        for (int i = 0 ; i < movableObjects.Count; i++) {
            if(buttonsState[movableObjects[i].name]) {
                Vector3 newPosition = Handles.PositionHandle(movableObjects[i].position, Quaternion.identity);
                if (newPosition != movableObjects[i].position) {
                    Undo.RecordObject(movableObjects[i], "algo se movió!");
                    movableObjects[i].position = newPosition;
                }
            }
        }
    }

    void OnSceneGUI() {
       DrawGizmos();
       // SceneView.RepaintAll();
    }
    
    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        if (chargeButtons()) {
            SceneView.RepaintAll();
        }
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
