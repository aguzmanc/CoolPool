using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(AddPointsPathEnemies))]
public class AddPointsPathEnemiesEditor : Editor {
    
    AddPointsPathEnemies Target { get => (AddPointsPathEnemies) target; }
    
    static bool isAddPointPathButtonPressed;
    static bool isMovePointsPathButtonPressed;
    static bool isDeletePointsPathButtonPressed;

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        isAddPointPathButtonPressed = GUILayout.Toggle(isAddPointPathButtonPressed , "Agregar puntos al path", "Button");
        isMovePointsPathButtonPressed = GUILayout.Toggle(isMovePointsPathButtonPressed , "Mover puntos del path", "Button");
        isDeletePointsPathButtonPressed = GUILayout.Toggle(isDeletePointsPathButtonPressed , "Borrar puntos del path", "Button");
        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void OnSceneGUI () {
        
        DrawLinesBeetwenPointsPath();
        if(isAddPointPathButtonPressed) {
            DisableAllButtonsInspectorOptions();
            isAddPointPathButtonPressed = true;
            DrawPointsToThePathEnemy();
            AddPointsToPathEnemy();
        }

        if(isMovePointsPathButtonPressed) {
            DisableAllButtonsInspectorOptions();
            isMovePointsPathButtonPressed = true;
            DrawButtonsToMovePointsPathEnemies();
        }

        if(isDeletePointsPathButtonPressed) {
            DisableAllButtonsInspectorOptions();
            isDeletePointsPathButtonPressed = true;
            DrawButtonsToDeletePointsPathEnemy();
        }
    }

    void AddPointsToPathEnemy() {
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if (Physics.Raycast(worldRay, out hitInfo)) {
            if (Event.current.type == EventType.MouseDown) {
                GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                Event.current.Use();
                
                List<Transform> listOfChilds = Target.GetComponent<FollowPath>().GetAllChildsPath();                
                if(listOfChilds == null) {
                    Target.GetComponent<FollowPath>().CreatePath();
                    Undo.RegisterCreatedObjectUndo(Target.GetComponent<FollowPath>().AddPointToPath(hitInfo.point), "Se agrego un punto al path");
                }

                else {
                    Undo.RegisterCreatedObjectUndo(Target.GetComponent<FollowPath>().AddPointToPath(hitInfo.point), "Se agrego un punto al path");
                }
                
            }
        }
    }

    void DrawPointsToThePathEnemy() {
        List<Transform> listOfChilds;
        Handles.color = Color.green;
        listOfChilds = Target.GetComponent<FollowPath>().GetAllChildsPath();
        
        if(listOfChilds == null)
            return;

        for(int i = 0; i < listOfChilds.Count; i++) {
            Handles.DrawSolidDisc(listOfChilds[i].transform.position, Vector3.up, 0.5f);
        }
    }

    void DrawLinesBeetwenPointsPath() {
        Handles.color = Color.black;
        List<Transform> listOfChilds;
        Transform previousPath = null;
        Transform connectedObject;
        Transform initPoint = null;

        listOfChilds = Target.GetComponent<FollowPath>().GetAllChildsPath();
        
        if(listOfChilds == null || listOfChilds.Count < 2)
            return;
        
        initPoint = listOfChilds[0];
        previousPath = listOfChilds[0];

        for (int j = 1 ; j < listOfChilds.Count; j++) {
            
            connectedObject = listOfChilds[j];
            Handles.DrawLine(previousPath.position, connectedObject.position);
            previousPath = listOfChilds[j];
        }

        Handles.DrawLine(previousPath.position, initPoint.position);
    }

    void DrawButtonsToMovePointsPathEnemies() {
        Handles.color = new Color(0, 0, 1, 1);
        List<Transform> listOfChilds;
        listOfChilds = Target.GetComponent<FollowPath>().GetAllChildsPath();
        if(listOfChilds == null)
            return;
        
        for (int j = 0 ; j < listOfChilds.Count; j++) {
            Handles.DrawSolidDisc(listOfChilds[j].transform.position, Vector3.up, 0.5f);
            Vector3 newPosition = Handles.FreeMoveHandle(listOfChilds[j].transform.position, Quaternion.identity, 0.5f,Vector3.up, 
                                                        Handles.CircleHandleCap);
            
            if (newPosition != listOfChilds[j].transform.position) {
                Undo.RecordObject(listOfChilds[j], "Se movio un punto");
                newPosition.y = listOfChilds[j].transform.position.y;
                listOfChilds[j].transform.position = newPosition;
            }
        }
    }

    void DrawButtonsToDeletePointsPathEnemy() {
        Handles.color = new Color(1, 0, 0, 1);
        List<Transform> listOfChilds;
        listOfChilds = Target.GetComponent<FollowPath>().GetAllChildsPath();
        if(listOfChilds == null)
            return;
            
        for (int j = 0 ; j < listOfChilds.Count; j++) {
            Handles.DrawSolidDisc(listOfChilds[j].transform.position, Vector3.up, 0.5f);        
            if (Handles.Button(listOfChilds[j].transform.position, Quaternion.Euler(90, 0,0),
                0.5f, 0.5f, Handles.CircleHandleCap)) {
                Target.GetComponent<FollowPath>().DeleteOneChildOfPath(j);
            }
        }
    }

    void DisableAllButtonsInspectorOptions() {
        isAddPointPathButtonPressed = false;
        isMovePointsPathButtonPressed = false;   
        isDeletePointsPathButtonPressed = false;
    }
}
