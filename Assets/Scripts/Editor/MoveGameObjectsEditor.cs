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
    static bool plane;
    static Transform planePos;
    Tool LastTool = Tool.None;

    public void OnEnable() {
        movableObjects = new List<Transform>();
        buttonsState = new Dictionary<string, bool>();
        ChargeAllTransforms(Target.transform);
        chargeDictionary();
        plane = false;
        LastTool = Tools.current;
        Tools.current = Tool.None;
    }

    void chargeDictionary() {
        for (int i = 0 ; i < movableObjects.Count; i++) {
            buttonsState.Add(movableObjects[i].name, false);
        }
    }

    bool chargeButtons() {
        bool newplane = GUILayout.Toggle(plane, "Mover Plane", "Button");
        plane = newplane;
        for (int i = 0 ; i < movableObjects.Count; i++) {
            bool newValue = GUILayout.Toggle(buttonsState[movableObjects[i].name], "Mover " + movableObjects[i].name, "Button");
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
        planePos = null;
         Tools.current = LastTool;
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
        if (plane){
            //Vector3 newPosition = Handles.PositionHandle(planePos.position, Quaternion.identity);
            Vector3 newPosition = Handles.ScaleHandle(planePos.localScale, Vector3.zero, Quaternion.identity, 5);
            if (newPosition != planePos.localScale) {
                Undo.RecordObject(planePos, "algo se movió!");
                planePos.localScale = newPosition;
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

    void ChargeAllTransforms(Transform transform) {    
        for (int i= 0; i < transform.childCount; i++){
            ChargeAllTransforms(transform.GetChild(i));
        }

        if (transform.GetComponent<IMoveObjects>() != null){
            movableObjects.Add(transform);
        }
        if (transform.GetComponent<Plane>() != null){
            planePos = transform;
        }
    }
}
