using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SandTrapManager)), CanEditMultipleObjects]
public class SandTrapManagerEditor : Editor {
    SandTrapManager Target { get => (SandTrapManager) target; }

void OnEnable() {
    Target.FindSandTraps();
    Undo.undoRedoPerformed += FixtheList;
    }

void OnDisable() {
    Target.ClearList();
}

public override void OnInspectorGUI() {
    DrawDefaultInspector();
    bool MoveSandTrap = GUILayout.Button("Move traps");
    

    if (MoveSandTrap) {



    }
}


void OnSceneGUI() {
        
    if (Target.SandTrapList == null || Target.SandTrapList.Count == 0) return;

    foreach (GameObject trap in Target.SandTrapList) {
        bool PresstheButton =
                Handles.Button(trap.transform.position
                + trap.transform.forward * 0.5f
                + trap.transform.right * 0.5f,
                Quaternion.Euler(90,0,0),
                0.5f, 0.5f,
                Handles.RectangleHandleCap); 

        DrawGizmos(trap);


        if (PresstheButton) {

            Undo.RecordObject(Target, "Sand trap destroyed");
            Target.SandTrapList.Remove(trap);
            Undo.DestroyObjectImmediate(trap);
            break;

            }
        }
    }

void DrawGizmos(GameObject trap) {

    EditorGUI.BeginChangeCheck();
    Vector3 newTargetPosition = Handles.PositionHandle(trap.transform.position, Quaternion.identity);
    if (EditorGUI.EndChangeCheck()) {

        Undo.RecordObject(trap, "A trap was moved");
        trap.transform.position = newTargetPosition;

    }


}

void FixtheList() {

    Target.ClearList();
    Target.FindSandTraps();

    }

}