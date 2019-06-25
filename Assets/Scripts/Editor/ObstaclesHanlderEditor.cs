using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(ObstaclesHandler))]
public class ObstaclesHandlerEditor : Editor {

    ObstaclesHandler Target { get => (ObstaclesHandler) target; }
    static bool isObstacleButtonPressed;

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        isObstacleButtonPressed = GUILayout.Toggle(isObstacleButtonPressed ,"Crear Obstaculo", "Button");
        
        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }



    void OnSceneGUI () {

        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if(isObstacleButtonPressed) {
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    //Undo.RegisterCreatedObjectUndo(Target.CreateObstacle(hitInfo.point), "Se creo un obstaculo");
                    Undo.RegisterCreatedObjectUndo( SafePrefabInstantiate(Target.GetPrefabReference(), hitInfo.point), "Se creo un obstaculo");
                }
            }
        }

        //DrawButtonsToDeleteEnemies();
        
        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }


    public GameObject SafePrefabInstantiate(GameObject reference, Vector3 position){
        return SafePrefabInstantiate(reference,position,Quaternion.identity);
    }


    public GameObject SafePrefabInstantiate (GameObject reference, 
                                                    Vector3 position,
                                                    Quaternion rotation) {
        #if UNITY_EDITOR
        if (Application.isPlaying) {
            return GameObject.Instantiate(reference, position, rotation);
        } else {
            GameObject obj = PrefabUtility.InstantiatePrefab(reference) as GameObject;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        #else
            return Instantiate(reference);
        #endif
    }

    // void DrawButtonsToDeleteEnemies() {
    //     Handles.color = new Color(1, 0, 0, 1);
    //     List<GameObject> enemiesList = Target.getEnemiesList();
        
    //     for(int i = 0; i < enemiesList.Count; i++) {
    //         Handles.DrawSolidDisc(enemiesList[i].transform.position, Vector3.up, 0.5f);    
    //         if (Handles.Button(enemiesList[i].transform.position, Quaternion.Euler(90, 0,0),
    //             0.5f, 0.5f, Handles.CircleHandleCap)) {
    //             Target.DestroyEnemy(enemiesList[i]);
    //         }
    //     }
    // }
}