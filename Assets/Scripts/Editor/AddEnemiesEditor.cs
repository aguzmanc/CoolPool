using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(AddEnemies))]
public class AddEnemiesEditor : Editor {

    AddEnemies Target { get => (AddEnemies) target; }
    
    public override void OnInspectorGUI () {
        if (GUILayout.Button("Crear enemigo!")) {
            Target.CreateEnemy(Target.positionIndicator.position);
        }

        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void OnSceneGUI () {
        DrawGizmos(Target);
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if (Physics.Raycast(worldRay, out hitInfo)) {
            Debug.Log(hitInfo.point);
            if (Event.current.type == EventType.MouseDown) {
                // GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                // Event.current.Use();
                Target.CreateEnemy(hitInfo.point);
            }
        }
        
        /*
        Handles.color = new Color(1,0,0, 1);
        
         Handles.DrawSolidDisc(Target.transform.position, Vector3.up, 0.5f);
        
        if (Handles.Button(Target.transform.position, Quaternion.Euler(90, 0,0),
                           0.5f, 0.5f, Handles.CircleHandleCap)) {
            Debug.Log("!!!!??");
        }
        */

        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    public static void DrawGizmos (AddEnemies customTarget) {
        Handles.matrix = customTarget.transform.localToWorldMatrix;
    }
}
