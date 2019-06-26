using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(AddEnemies))]
public class AddEnemiesEditor : Editor {

    AddEnemies Target { get => (AddEnemies) target; }
    static bool isEnemyCreateButtonPressed;
    static bool isPathDeleteButtonPressed;
    static bool isPathMoveButtonPressed;
    static bool isEnemyDeleteButtonPressed;
    
    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        isEnemyCreateButtonPressed = GUILayout.Toggle(isEnemyCreateButtonPressed ,"Crear enemigo", "Button");
        isPathDeleteButtonPressed = GUILayout.Toggle(isPathDeleteButtonPressed ,"Eliminar puntos de los paths", "Button");
        isPathMoveButtonPressed = GUILayout.Toggle(isPathMoveButtonPressed ,"Mover puntos de los paths", "Button");
        isEnemyDeleteButtonPressed = GUILayout.Toggle(isEnemyDeleteButtonPressed ,"Eliminar enemigos", "Button");
        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void updateListEnemies() {
        Target.udpateListEnemies();
    }

    void OnSceneGUI () {
        updateListEnemies();
        DrawPaths();
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if(isEnemyCreateButtonPressed) {
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    Undo.RegisterCreatedObjectUndo(Target.CreateEnemy(hitInfo.point), "Se creo un enemigo");
                }
            }
        }

        if(isPathDeleteButtonPressed) {
            DrawButtonsToDeletePathEnemies();
        }

        if(isEnemyDeleteButtonPressed) {
            DrawButtonsToDeleteEnemies();
        }
        
        if(isPathMoveButtonPressed) {
            DrawButtonsToMovePathEnemies();
        }
        
        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void DrawButtonsToMovePathEnemies() {
        Handles.color = new Color(0, 1, 0, 1);
        List<GameObject> enemiesList = Target.GetEnemiesList();
        List<Transform> listOfChilds;
        for(int i = 0; i < enemiesList.Count; i++) {
            listOfChilds = enemiesList[i].GetComponent<FollowPath>().GetAllChildsPath();
            if(listOfChilds == null)
                continue;

            for (int j = 0 ; j < listOfChilds.Count; j++) {
                Handles.DrawSolidDisc(listOfChilds[j].transform.position, Vector3.up, 0.5f);        
                // Vector3 newPosition = Handles.PositionHandle(listOfChilds[j].transform.position, Quaternion.identity);
                Vector3 newPosition = Handles.FreeMoveHandle(listOfChilds[j].transform.position, Quaternion.identity, 0.5f,Vector3.up, 
                                                            Handles.CircleHandleCap);
                
                if (newPosition != listOfChilds[j].transform.position) {
                    // Undo.RegisterCreatedObjectUndo(listOfChilds[j], "algo se movió!");
                    newPosition.y = listOfChilds[j].transform.position.y;
                    listOfChilds[j].transform.position = newPosition;
                }
            }
        }
    }

    void DrawButtonsToDeletePathEnemies() {
        Handles.color = new Color(1, 0, 0, 1);
        List<GameObject> enemiesList = Target.GetEnemiesList();
        List<Transform> listOfChilds;
        for(int i = 0; i < enemiesList.Count; i++) {
            listOfChilds = enemiesList[i].GetComponent<FollowPath>().GetAllChildsPath();
            if(listOfChilds == null)
                continue;

            for (int j = 0 ; j < listOfChilds.Count; j++) {
                Handles.DrawSolidDisc(listOfChilds[j].transform.position, Vector3.up, 0.5f);        
                if (Handles.Button(listOfChilds[j].transform.position, Quaternion.Euler(90, 0,0),
                    0.5f, 0.5f, Handles.CircleHandleCap)) {
                    enemiesList[i].GetComponent<FollowPath>().DeleteOneChildOfPath(j);
                }
            }
        }
    }
    
    void DrawButtonsToDeleteEnemies() {
        Handles.color = new Color(1, 0, 0, 1);
        List<GameObject> enemiesList = Target.GetEnemiesList();
        for (int i = 0 ; i < enemiesList.Count; i++) {
            Handles.DrawSolidDisc(enemiesList[i].transform.position, Vector3.up, 0.5f);
            if (Handles.Button(enemiesList[i].transform.position, Quaternion.Euler(90, 0,0),
                0.5f, 0.5f, Handles.CircleHandleCap)) {
                Target.DestroyEnemy(enemiesList[i]);
            }
        }
    }

    void DrawPaths() {
        Handles.color = Color.black;
        List<GameObject> enemiesList = Target.GetEnemiesList();
        List<Transform> listOfChilds;
        Transform previousPath = null;
        Transform connectedObject;
        Transform init = null;

        for (int i = 0 ; i < enemiesList.Count; i++) {
            listOfChilds = enemiesList[i].GetComponent<FollowPath>().GetAllChildsPath();
            
            if(listOfChilds == null)
                continue;
            
            for (int j = 0 ; j < listOfChilds.Count; j++) {
                if(listOfChilds[j] == null)
                    continue;
                
                connectedObject = listOfChilds[j];
                if(j == 0) {
                    init = listOfChilds[j];
                    previousPath = listOfChilds[j];
                    continue;
                }

                else {
                    Handles.DrawLine(previousPath.position, connectedObject.position);

                }
                
                previousPath = listOfChilds[j];
            }
            Handles.DrawLine(previousPath.position, init.position);
        }
    }
}
