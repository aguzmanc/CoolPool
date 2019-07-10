using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SandTrapManager))]
public class SandTrapManagerEditor : Editor {
    SandTrapManager Target { get => (SandTrapManager) target; }

void OnEnable() {
    Target.FindSandTraps();
    }
void OnDisable() {
    Target.ClearList();
}

public override void OnInspectorGUI() {
    DrawDefaultInspector();

/*    bool wasPressed = GUILayout.Button("CLEAR");
    if (wasPressed) {
        Target.ClearList();
    }
*/

}

void OnSceneGUI() {
        
    if (Target.SandTrapList == null || Target.SandTrapList.Count == 0) return;

    foreach (GameObject trap in Target.SandTrapList) {
        bool ButtonOnSandTraps =
                Handles.Button(trap.transform.position
                + trap.transform.forward * 0.5f
                + trap.transform.right * 0.5f,
                Quaternion.Euler(90,0,0),
                0.25f, 0.25f,
                Handles.RectangleHandleCap); 
        }
    }
}