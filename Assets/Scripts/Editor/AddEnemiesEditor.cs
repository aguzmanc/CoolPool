using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(AddEnemies))]
public class AddEnemiesEditor : Editor {

    AddEnemies Target { get => (AddEnemies) target; }
    static bool isEnemyButtonPressed;

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        isEnemyButtonPressed = GUILayout.Toggle(isEnemyButtonPressed ,"Crear enemigo", "Button");
        
        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void OnSceneGUI () {
        // DrawGizmos(Target);
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if(isEnemyButtonPressed) {
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    //Como darle cmd+z para que un objecto creado se borre ?
                    Undo.RecordObject(Target.CreateEnemy(hitInfo.point), "Se creo un enemigo");
                }
            }
        }
        
        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    // public static void DrawGizmos (AddEnemies customTarget) {
    //     Handles.matrix = customTarget.transform.localToWorldMatrix;
    // }
}
