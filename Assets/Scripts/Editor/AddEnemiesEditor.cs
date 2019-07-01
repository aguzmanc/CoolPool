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
    static List<bool> isAddPointToPathEnemyPressed = new List<bool>();

    public void OnEnable() {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        
        int enemiesListLength = enemiesList.Count;
        int enemiesPointPathButtonsLength = isAddPointToPathEnemyPressed.Count;
        if((enemiesListLength - enemiesPointPathButtonsLength) > 0) {
            for(int i = 0; i < (enemiesListLength - enemiesPointPathButtonsLength); i++) {
                isAddPointToPathEnemyPressed.Add(false);
            }
        }
        
    }

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        isEnemyCreateButtonPressed = GUILayout.Toggle(isEnemyCreateButtonPressed ,"Crear enemigo", "Button");
        isEnemyDeleteButtonPressed = GUILayout.Toggle(isEnemyDeleteButtonPressed ,"Eliminar enemigos", "Button");
        isPathMoveButtonPressed = GUILayout.Toggle(isPathMoveButtonPressed ,"Mover puntos de los paths", "Button");
        isPathDeleteButtonPressed = GUILayout.Toggle(isPathDeleteButtonPressed ,"Eliminar puntos de los paths", "Button");
        
        DrawButtonsToAddPointsToPathEnemies();

        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
        
    }

    void DrawButtonsToAddPointsToPathEnemies() {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        for(int i = 0; i < enemiesList.Count; i++) {
            isAddPointToPathEnemyPressed[i] = GUILayout.Toggle(isAddPointToPathEnemyPressed[i], "Agregar puntos al path del enemigo: " + (i + 1), "Button");
        }
    }

    void updateListEnemies() {
        Target.udpateListEnemies();
    }

    void OnSceneGUI () {
        updateListEnemies();
        DrawLinesPaths();
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if(isEnemyCreateButtonPressed) {
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    Undo.RegisterCreatedObjectUndo(Target.CreateEnemy(hitInfo.point), "Se creo un enemigo");
                    isAddPointToPathEnemyPressed.Add(false);
                }
            }
        }

        AddPointsToPathByEnemy();

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

    void AddPointsToPathByEnemy() {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        Handles.color = Color.black;
        for(int i = 0; i < isAddPointToPathEnemyPressed.Count; i++) {
            if(isAddPointToPathEnemyPressed[i]) {
                DrawPointsToThePathEnemy(i);

                if (Physics.Raycast(worldRay, out hitInfo)) {

                    if (Event.current.type == EventType.MouseDown) {
                        
                        GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                        Event.current.Use();
                        List<Transform> listOfChilds = enemiesList[i].GetComponent<FollowPath>().GetAllChildsPath();
                        
                        if(listOfChilds == null) {
                            enemiesList[i].GetComponent<FollowPath>().CreatePath();
                            Undo.RegisterCreatedObjectUndo(enemiesList[i].GetComponent<FollowPath>().AddPointToPath(hitInfo.point), "Se agrego un punto al path");
                        }

                        else {
                            Undo.RegisterCreatedObjectUndo(enemiesList[i].GetComponent<FollowPath>().AddPointToPath(hitInfo.point), "Se agrego un punto al path");
                        }
                    }
                }
            }

        }
    }

    void DrawPointsToThePathEnemy(int index) {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        List<Transform> listOfChilds;
        listOfChilds = enemiesList[index].GetComponent<FollowPath>().GetAllChildsPath();
        
        Handles.DrawSolidDisc(enemiesList[index].transform.position, Vector3.up, 0.5f);
        if(listOfChilds == null)
            return;

        for(int i = 0; i < listOfChilds.Count; i++) {
            Handles.DrawSolidDisc(listOfChilds[i].transform.position, Vector3.up, 0.5f);
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

    void DrawLinesPaths() {
        Handles.color = Color.black;
        List<GameObject> enemiesList = Target.GetEnemiesList();
        List<Transform> listOfChilds;
        Transform previousPath = null;
        Transform connectedObject;
        Transform init = null;

        
        for (int i = 0 ; i < enemiesList.Count; i++) {
            listOfChilds = enemiesList[i].GetComponent<FollowPath>().GetAllChildsPath();
            
            if(listOfChilds == null || listOfChilds.Count < 2)
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
