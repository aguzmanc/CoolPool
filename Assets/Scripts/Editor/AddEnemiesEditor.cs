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

    void updateListEnemies() {
        Target.udpateListEnemies();
    }

    void OnSceneGUI () {
        //actulizar lista
        updateListEnemies();
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if(isEnemyButtonPressed) {
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    Undo.RegisterCreatedObjectUndo(Target.CreateEnemy(hitInfo.point), "Se creo un enemigo");
                }
            }
        }

        DrawButtonsToDeleteEnemies();
        
        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void DrawButtonsToDeleteEnemies() {
        Handles.color = new Color(1, 0, 0, 1);
        List<GameObject> enemiesList = Target.getEnemiesList();
        
        for(int i = 0; i < enemiesList.Count; i++) {
            Handles.DrawSolidDisc(enemiesList[i].transform.position, Vector3.up, 0.5f);    
            if (Handles.Button(enemiesList[i].transform.position, Quaternion.Euler(90, 0,0),
                0.5f, 0.5f, Handles.CircleHandleCap)) {
                Target.DestroyEnemy(enemiesList[i]);
            }
        }
    }
}
