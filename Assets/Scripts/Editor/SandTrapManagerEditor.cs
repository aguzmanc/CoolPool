using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SandTrapManager)), CanEditMultipleObjects]
public class SandTrapManagerEditor : Editor {

public static bool MoveSandTrap;
public static bool CreateSandTrap;
public static bool DeleteSandTrap;

    SandTrapManager Target { get => (SandTrapManager) target; }

void OnEnable() {
    FixtheList();
    Undo.undoRedoPerformed += FixtheList;
    }

void OnDisable() {
    Target.ClearList();
}

public override void OnInspectorGUI() {
    DrawDefaultInspector();
    MoveSandTrap = GUILayout.Toggle(MoveSandTrap, "Move traps", "Button");
    CreateSandTrap = GUILayout.Toggle(CreateSandTrap, "Create traps", "Button");
    DeleteSandTrap = GUILayout.Toggle(DeleteSandTrap, "Delete traps", "Button");

    if (MoveSandTrap) {
        SceneView.RepaintAll();
    }

    if (CreateSandTrap) {
        SceneView.RepaintAll();
    }

    if (DeleteSandTrap) {
        SceneView.RepaintAll();
    }

}


void OnSceneGUI() {
        
    if (Target.SandTrapList == null || Target.SandTrapList.Count == 0) return;
    
    if (DeleteSandTrap) {
        DrawDeleteSquares();
    }

    if (CreateSandTrap) {
        EnableCreateMode();
    }

    if (MoveSandTrap) {
        DrawGizmos();
    }
    
}

void DrawGizmos() {

    foreach (GameObject trap in Target.SandTrapList) {
        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(trap.transform.position, Quaternion.identity);
        
        if (EditorGUI.EndChangeCheck()) {
            
            Undo.RecordObject(trap.transform, "A trap was moved");
            trap.transform.position = newTargetPosition;
            
            }
        }
    }

void EnableCreateMode () {

Ray rayo = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        bool didHit = Physics.Raycast(rayo, out hit);
        bool isClicking =
            Event.current.type == EventType.MouseDown &&
            Event.current.button == 0;

        if (didHit && isClicking) {
            GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
            Event.current.Use();
            GameObject newTrap = Instantiate(Target.TrapPrefab);
            newTrap.transform.position = hit.point;

            Undo.RegisterCreatedObjectUndo(newTrap, "Game object created");
        }


    FixtheList();
}


void DrawDeleteSquares() {
    foreach (GameObject trap in Target.SandTrapList) {
      bool PresstheButton =
            Handles.Button(trap.transform.position
                + trap.transform.forward * 0.5f
                + trap.transform.right * 0.5f,
                Quaternion.Euler(90,0,0),
                0.5f, 0.5f,
                Handles.RectangleHandleCap);
        
        if (PresstheButton) {

            Undo.RecordObject(Target, "Sand trap destroyed");
            Target.SandTrapList.Remove(trap);
            Undo.DestroyObjectImmediate(trap);
            break;

            }
    }
}
void FixtheList() {

    Target.ClearList();
    Target.FindSandTraps();
        }
}